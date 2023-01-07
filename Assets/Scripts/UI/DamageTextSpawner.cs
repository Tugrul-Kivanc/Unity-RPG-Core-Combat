using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private DamageText damageTextPrefab;

        public void Spawn(float damageAmount)
        {
            DamageText damageText = Instantiate<DamageText>(damageTextPrefab, transform);
            damageText.SetText(damageAmount);
        }
    }
}
