using System;
using GameDevTV.Inventories;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create New Scriptable Weapon", order = 0)]
    public class WeaponConfig : EquipableItem
    {
        [SerializeField] private AnimatorOverrideController animatorOverride = null;
        [SerializeField] private Weapon equippedWeaponPrefab = null;
        [SerializeField] private bool isRightHanded = true;
        [SerializeField] private Projectile projectile = null;
        [SerializeField] private float range = 2f;
        [SerializeField] private float baseDamage = 5f;
        [SerializeField] private float multiplicativeBonus = 0f;
        public float Range => range;
        public float BaseDamage => baseDamage;
        public float MultiplicativeBonus => multiplicativeBonus;
        private const string weaponName = "Weapon";
        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            Weapon weapon = null;
            if (equippedWeaponPrefab != null)
            {
                weapon = Instantiate(equippedWeaponPrefab, GetHandTransform(rightHand, leftHand));
                weapon.gameObject.name = weaponName;
            }

            SetupAnimator(animator);

            return weapon;
        }

        private void SetupAnimator(Animator animator)
        {
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
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

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }
    }
}