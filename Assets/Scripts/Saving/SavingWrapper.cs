using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SavingSystem
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
                GetComponent<SavingSystem>().Save(defaultSaveFile);
            if (Input.GetKeyDown(KeyCode.F9))
                GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}
