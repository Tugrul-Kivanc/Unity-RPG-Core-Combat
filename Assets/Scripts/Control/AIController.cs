using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 6f;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointGuardingDuration = 4f;
        [SerializeField] PatrolPath patrolPath;
        private GameObject player;
        private Fighter fighter;
        private Health health;
        private Mover mover;
        private Vector3 initialGuardPosition;
        float timeSincePlayerSeen = Mathf.Infinity;
        float timeSinceArriveAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            initialGuardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (IsPlayerInAttackRange() && fighter.CanAttack(player))
            {
                AttackBehavior();
            }
            else if (timeSincePlayerSeen < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSincePlayerSeen += Time.deltaTime;
            timeSinceArriveAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = initialGuardPosition;
            if (patrolPath != null)
            {
                if (IsAtWaypoint())
                {
                    timeSinceArriveAtWaypoint = 0;
                    CycleWaypoint();
                }
                else
                {
                    nextPosition = GetCurrentWaypoint();
                }
            }
            if (timeSinceArriveAtWaypoint > waypointGuardingDuration)
                mover.StartMoveAction(nextPosition);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndexCircular(currentWaypointIndex);
        }

        private bool IsAtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            timeSincePlayerSeen = 0;
            fighter.Attack(player);
        }

        private bool IsPlayerInAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}