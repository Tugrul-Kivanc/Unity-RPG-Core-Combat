using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] private GameObject objectToDestroy;

        public void DestroyObject()
        {
            Destroy(objectToDestroy);
        }
    }
}
