using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace RPG.SavingSystem
{
    public class SavingSystem : MonoBehaviour
    {
        private string saveFileExtension = ".sav";
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                Transform playerTransform = GameObject.FindWithTag("Player").transform;

                byte[] buffer = SerializeVector(playerTransform.position);
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                Transform playerTransform = GameObject.FindWithTag("Player").transform;
                playerTransform.position = DeserializeVector(buffer);

            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + saveFileExtension);
        }

        private byte[] SerializeVector(Vector3 vector)
        {
            byte[] vectorBytes = new byte[3 * sizeof(float)];

            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, sizeof(float) * 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, sizeof(float) * 1);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, sizeof(float) * 2);

            return vectorBytes;
        }

        private Vector3 DeserializeVector(byte[] buffer)
        {
            Vector3 vector = new Vector3();

            vector.x = BitConverter.ToSingle(buffer, sizeof(float) * 0);
            vector.y = BitConverter.ToSingle(buffer, sizeof(float) * 1);
            vector.z = BitConverter.ToSingle(buffer, sizeof(float) * 2);

            return vector;
        }
    }
}