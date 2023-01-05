using System.Collections;
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

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, CalculateLevel());
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return initialLevel;

            float experiencePoints = experience.GetExperiencePoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int lvl = 1; lvl <= penultimateLevel; lvl++)
            {
                float experienceToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, lvl);
                if (experienceToLevelUp > experiencePoints)
                {
                    return lvl;
                }
            }
            level = penultimateLevel + 1;
            return level;
        }

        public int GetLevel()
        {
            return level;
        }
    }
}
