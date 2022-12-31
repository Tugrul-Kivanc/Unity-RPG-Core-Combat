using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.SavingSystem
{
    public class SavingSystem : MonoBehaviour
    {
        string saveFileExtension = ".sav";
        //TODO binaryformatter is insecure
        //https://gitlab.com/Mnemoth42/RPG/-/wikis/home
        BinaryFormatter formatter = new BinaryFormatter();
        Transform player;
        void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                formatter.Serialize(stream, CaptureState());
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                RestoreState(formatter.Deserialize(stream));
            }
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + saveFileExtension);
        }

        object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (var saveableEntity in FindObjectsOfType<SaveableEntity>())
            {
                state[saveableEntity.GetUniqueIdentifier()] = saveableEntity.CaptureState();
            }
            return state;
        }

        void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (var saveableEntity in FindObjectsOfType<SaveableEntity>())
            {
                saveableEntity.RestoreState(stateDict[saveableEntity.GetUniqueIdentifier()]);
            }
        }
    }
}