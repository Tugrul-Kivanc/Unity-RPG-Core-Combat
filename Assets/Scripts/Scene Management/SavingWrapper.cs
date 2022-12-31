using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFileName = "save";
        [SerializeField] float fadeInTime = 0.5f;
        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFileName);
            yield return fader.FadeIn(fadeInTime);
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
