using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string defaultSaveFileName = "save";
        [SerializeField] private float fadeInTime = 0.5f;
        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFileName);
            yield return fader.FadeIn(fadeInTime);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
                Save();
            if (Input.GetKeyDown(KeyCode.F9))
                Load();
            if (Input.GetKeyDown(KeyCode.Delete))
                Delete();
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFileName);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFileName);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFileName);
        }
    }
}
