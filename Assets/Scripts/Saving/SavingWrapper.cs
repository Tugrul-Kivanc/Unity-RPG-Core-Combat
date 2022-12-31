using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SavingSystem
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFileName = "save";
        void Start()
        {

        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
                Save();
            if (Input.GetKeyDown(KeyCode.F9))
                Load();
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFileName);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFileName);
        }
    }
}
