using System;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Create New Scriptable Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        [Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;
        }

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionCharacterClass in characterClasses)
            {
                if (progressionCharacterClass.characterClass == characterClass)
                {
                    return progressionCharacterClass.health[level - 1];
                }
            }
            return 0;
        }
    }
}