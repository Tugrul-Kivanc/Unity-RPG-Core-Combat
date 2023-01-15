using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Stats;
using UnityEngine;

namespace RPG.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item/Create New Equipable Item", order = 0)]
    public class StatsEquipableItem : EquipableItem, IModifierProvider
    {
        [SerializeField] private Modifier[] additiveModifiers;
        [SerializeField] private Modifier[] multiplicativeModifiers;

        [System.Serializable]
        struct Modifier
        {
            public Stat stat;
            public float Value;
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (var modifier in additiveModifiers)
            {
                if (modifier.stat == stat) yield return modifier.Value;
            }
        }

        public IEnumerable<float> GetMultiplicativeMofidifiers(Stat stat)
        {
            foreach (var modifier in multiplicativeModifiers)
            {
                if (modifier.stat == stat) yield return modifier.Value;
            }
        }
    }
}