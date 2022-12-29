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
        [Range(0, 1)][SerializeField] float patrolSpeedFraction = 0.5f;
        [SerializeField] PatrolPath patrolPath;
        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
        Vector3 initialGuardPosition;
        float timeSincePlayerSeen = Mathf.Infinity;
        float timeSinceArriveAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            initialGuardPosition = transform.position;
        }

        void Update()
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

        void UpdateTimers()
        {
            timeSincePlayerSeen += Time.deltaTime;
            timeSinceArriveAtWaypoint += Time.deltaTime;
        }

        void PatrolBehavior()
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
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }

        Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndexCircular(currentWaypointIndex);
        }

        bool IsAtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void AttackBehavior()
        {
            timeSincePlayerSeen = 0;
            fighter.Attack(player);
        }

        bool IsPlayerInAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}