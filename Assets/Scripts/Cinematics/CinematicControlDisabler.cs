﻿using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlDisabler : MonoBehaviour
    {
        GameObject player;
        void Start()
        {
            GetComponent<PlayableDirector>().played += DisableConrtol;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            player = GameObject.FindWithTag("Player");
        }

        void DisableConrtol(PlayableDirector playableDirector)
        {
            print("Disabled Controls");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector playableDirector)
        {
            print("Enabled Controls");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}