using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        private BaseStats baseStats;
        private TextMeshProUGUI experienceText;
        // Start is called before the first frame update
        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            experienceText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        private void Update()
        {
            experienceText.text = baseStats.CalculateLevel().ToString();
        }
    }
}
