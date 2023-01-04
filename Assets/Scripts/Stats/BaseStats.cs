﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 60)][SerializeField] int level = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        int initialLevel = 1;

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, level);
        }

        public float GetExperienceReward()
        {
            return 10f;
        }
    }
}
