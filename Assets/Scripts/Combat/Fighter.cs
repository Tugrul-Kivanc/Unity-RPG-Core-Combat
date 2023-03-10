using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;
using System.Collections.Generic;
using GameDevTV.Utils;
using System;
using GameDevTV.Inventories;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private Transform rightHandTransform = null;
        [SerializeField] private Transform leftHandTransform = null;
        [SerializeField] private WeaponConfig defaultWeapon = null;
        private Health target;
        private Mover mover;
        private Equipment equipment;
        private WeaponConfig currentWeaponConfig;
        private LazyValue<Weapon> currentWeapon;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            equipment = GetComponent<Equipment>();
            currentWeaponConfig = defaultWeapon;
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
        }

        private void OnEnable()
        {
            if (equipment)
            {
                equipment.equipmentUpdated += UpdateEquippedWeapon;
            }
        }

        private Weapon SetupDefaultWeapon()
        {
            return AttachWeapon(defaultWeapon);
        }

        private void Start()
        {
            AttachWeapon(currentWeaponConfig);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!IsTargetInRange(target.transform))
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit() event.
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        #region Animation Events
        private void Hit()
        {
            if (target == null || target.IsDead()) return;

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);

            if (currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }

            if (!currentWeaponConfig.IsRangedWeapon())
            {

                target.TakeDamage(gameObject, damage);
            }
            else
            {
                currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
        }

        private void Shoot()
        {
            Hit();
        }
        #endregion

        private bool IsTargetInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) < currentWeaponConfig.Range;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            if (!mover.CanMoveTo(combatTarget.transform.position)
                && IsTargetInRange(combatTarget.transform)) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return !(targetToTest == null || targetToTest.IsDead());
        }

        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon;
            currentWeapon.value = AttachWeapon(weapon);
        }

        private void UpdateEquippedWeapon()
        {
            WeaponConfig weaponConfig = equipment.GetItemInSlot(EquipLocation.Weapon) as WeaponConfig;

            if (weaponConfig == null) EquipWeapon(defaultWeapon);
            else EquipWeapon(weaponConfig);
        }

        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public object CaptureState()
        {
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            //Use addressables on later versions for saving.
            //https://docs.unity3d.com/Manual/com.unity.addressables.html
            string weaponName = (string)state;
            var weapon = Resources.Load(weaponName) as WeaponConfig;
            EquipWeapon(weapon);
        }

        public Health GetTarget()
        {
            return target;
        }
    }
}
