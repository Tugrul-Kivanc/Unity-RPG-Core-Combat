using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create New Scriptable Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedWeaponPrefab = null;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        [SerializeField] float range = 2f;
        public float Range => range;
        [SerializeField] float damage = 5f;
        public float Damage => damage;
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedWeaponPrefab != null)
            {
                Instantiate(equippedWeaponPrefab, GetHandTransform(rightHand, leftHand));
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            return isRightHanded ? rightHand : leftHand;
        }

        public bool IsRangedWeapon()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target);
        }
    }
}