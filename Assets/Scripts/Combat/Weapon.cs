using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create New Scriptable Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedWeaponPrefab = null;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] float range = 2f;
        public float Range => range;
        [SerializeField] float damage = 5f;
        public float Damage => damage;
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            Transform hand = isRightHanded ? rightHand : leftHand;

            if (equippedWeaponPrefab != null)
            {
                Instantiate(equippedWeaponPrefab, hand);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}