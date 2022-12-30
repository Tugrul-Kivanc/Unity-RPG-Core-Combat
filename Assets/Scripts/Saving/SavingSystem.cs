using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SavingSystem
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            print("Saving to " + saveFile);
        }

        public void Load(string loadFile)
        {
            print("Loading from " + loadFile);
        }
    }
}