using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create New Scriptable Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedWeaponPrefab = null;
        [SerializeField] float range = 2f;
        public float Range => range;
        [SerializeField] float damage = 5f;
        public float Damage => damage;
        public void Spawn(Transform handTransform, Animator animator)
        {
            if (equippedWeaponPrefab != null)
            {
                Instantiate(equippedWeaponPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}