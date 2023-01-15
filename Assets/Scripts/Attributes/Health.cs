using System;
using GameDevTV.Inventories;
using GameDevTV.Utils;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public partial class Health : MonoBehaviour, ISaveable
    {
        [Range(0f, 1f)][SerializeField] private float levelUpHealthRegenFraction = 0.9f;
        [SerializeField] private TakeDamageEvent onTakeDamage;
        [SerializeField] private UnityEvent onDie;
        private bool isDead = false;
        private Equipment equipment;
        private LazyValue<float> healthPoints;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
            equipment = GetComponent<Equipment>();
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            healthPoints.ForceInitialize();
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += IncreaseMaxHealthOnLevelUp;
            if (equipment != null) equipment.equipmentUpdated += UpdateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= IncreaseMaxHealthOnLevelUp;
            if (equipment != null) equipment.equipmentUpdated -= UpdateHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            if (healthPoints.value <= 0)
            {
                onDie?.Invoke();
                Die();
                AwardExperience(instigator);
            }
            else
            {
                onTakeDamage?.Invoke(damage);
            }
        }

        private void IncreaseMaxHealthOnLevelUp()
        {
            healthPoints.value = GetComponent<BaseStats>().GetStat(Stat.Health) * levelUpHealthRegenFraction;
        }

        private void UpdateHealth()
        {
            healthPoints.value = Mathf.Min(healthPoints.value, GetMaxHealth());
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetHealth()
        {
            return healthPoints.value;
        }

        public float GetMaxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetHealthFraction()
        {
            return GetHealth() / GetMaxHealth();
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            if (healthPoints.value <= 0)
            {
                Die();
            }
        }

        public void Heal(float healthToRestore)
        {
            healthPoints.value = Mathf.Min(GetMaxHealth(), healthPoints.value + healthToRestore);
        }
    }
}