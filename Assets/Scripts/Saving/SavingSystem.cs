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
                SerializableVector3 serializablePosition = new SerializableVector3(player.position);
                formatter.Serialize(stream, serializablePosition);
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                SerializableVector3 serializedPosition = (SerializableVector3)formatter.Deserialize(stream);
                player.position = serializedPosition.ToVector();
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + saveFileExtension);
        }
    }
}