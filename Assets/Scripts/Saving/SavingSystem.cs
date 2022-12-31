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
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                formatter.Serialize(stream, state);
            }
        }

        Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path)) return new Dictionary<string, object>();

            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + saveFileExtension);
        }

        void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveableEntity in FindObjectsOfType<SaveableEntity>())
            {
                state[saveableEntity.GetUniqueIdentifier()] = saveableEntity.CaptureState();
            }
        }

        void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveableEntity in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveableEntity.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveableEntity.RestoreState(state[id]);
                }
            }
        }
    }
}