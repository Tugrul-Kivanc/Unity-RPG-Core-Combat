using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item/Create New Item Drop List", order = 0)]
    public class DropLibrary : ScriptableObject
    {
        [SerializeField] private float[] dropChance;
        [SerializeField] private int[] minDrops;
        [SerializeField] private int[] maxDrops;
        [SerializeField] DropConfig[] potentialDrops;

        [System.Serializable]
        class DropConfig
        {
            public InventoryItem item;
            public float[] relativeDropChance;
            public int[] minNumber;
            public int[] maxNumber;
            public int GetRandomNumber(int level)
            {
                if (!item.IsStackable()) return 1;

                return Random.Range(GetByLevel(minNumber, level), GetByLevel(maxNumber, level) + 1);
            }
        }

        public struct DroppedItem
        {
            public InventoryItem item;
            public int number;
        }

        public IEnumerable<DroppedItem> GetRandomDrops(int level)
        {
            if (!ShouldRandomDrop(level)) yield break;

            for (int i = 0; i < GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }
        }

        private bool ShouldRandomDrop(int level)
        {
            return Random.Range(0, 100) < GetByLevel(dropChance, level);
        }

        private int GetRandomNumberOfDrops(int level)
        {
            return Random.Range(GetByLevel(minDrops, level), GetByLevel(maxDrops, level));
        }

        private DroppedItem GetRandomDrop(int level)
        {
            var drop = SelectRandomItem(level);
            var droppedItem = new DroppedItem();

            droppedItem.item = drop.item;
            droppedItem.number = drop.GetRandomNumber(level);

            return droppedItem;
        }

        private DropConfig SelectRandomItem(int level)
        {
            float totalChance = GetTotalChange(level);
            float randomRoll = Random.Range(0, totalChance);
            float chanceTotal = 0;
            foreach (var drop in potentialDrops)
            {
                chanceTotal += GetByLevel(drop.relativeDropChance, level);

                if (chanceTotal > randomRoll) return drop;
            }
            return null;
        }

        private float GetTotalChange(int level)
        {
            float totalChance = 0;

            foreach (var drop in potentialDrops)
            {
                totalChance += GetByLevel(drop.relativeDropChance, level);
            }

            return totalChance;
        }

        private static T GetByLevel<T>(T[] values, int level)
        {
            if (values.Length == 0) return default;
            if (level > values.Length) return values[values.Length - 1]; //take the last element if level exceeds
            if (level <= 0) return default;

            return values[level - 1];
        }
    }
}
