using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        private GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }
        private void Update()
        {
            if (DistanceToPlayer(player) < chaseDistance)
            {
                print(gameObject.name + " is chasing player");
            }
        }

        private float DistanceToPlayer(GameObject player)
        {

            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
}