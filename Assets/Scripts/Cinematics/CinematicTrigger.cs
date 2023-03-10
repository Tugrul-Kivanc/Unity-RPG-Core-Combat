using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool isTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (!isTriggered && other.CompareTag("Player"))
            {
                isTriggered = true;
                GetComponent<PlayableDirector>().Play();
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
