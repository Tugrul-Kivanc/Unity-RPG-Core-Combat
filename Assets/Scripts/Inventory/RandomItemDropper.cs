using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Attributes;
using RPG.Stats;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Inventory
{
    public class RandomItemDropper : ItemDropper
    {
        [SerializeField] float maxDropDistance = 1f;
        [SerializeField] DropLibrary dropLibrary;
        private float sampleDistance = 0.1f;
        private const int randomAttempts = 10;

        public void DropRandomItem()
        {
            var randomDrops = dropLibrary.GetRandomDrops(GetComponent<BaseStats>().GetLevel());

            foreach (var drop in randomDrops)
            {
                DropItem(drop.item, drop.number);
            }
        }

        protected override Vector3 GetDropLocation()
        {
            for (int i = 0; i < randomAttempts; i++)
            {
                Vector3 dropLocation = transform.position + Random.insideUnitSphere * maxDropDistance;

                //Prevent spawning items in air
                NavMeshHit hit;
                if (NavMesh.SamplePosition(dropLocation, out hit, sampleDistance, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            return transform.position;
        }
    }
}