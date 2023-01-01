using System;
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
        const string weaponName = "Weapon";
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (equippedWeaponPrefab != null)
            {
                GameObject weapon = Instantiate(equippedWeaponPrefab, GetHandTransform(rightHand, leftHand));
                weapon.name = weaponName;
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DestroyingWeapon";
            Destroy(oldWeapon.gameObject);
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
            projectileInstance.SetTarget(target, damage);
        }
    }
}