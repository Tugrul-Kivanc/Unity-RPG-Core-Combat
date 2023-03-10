using System;
using RPG.Combat;
using RPG.Attributes;
using RPG.Movement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float maxNavMeshProjectionDistance = 1f;
        [SerializeField] private float sphereCastRadius = 0.5f;
        private Health health;
        private Cursors cursors;
        private Mover mover;
        private bool isDraggingUI = false;

        private void Awake()
        {
            health = GetComponent<Health>();
            cursors = FindObjectOfType<Cursors>();
            mover = GetComponent<Mover>();
        }
        private void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead())
            {
                cursors.NoneCursor.SetCursor();
                return;
            }
            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;

            cursors.NoneCursor.SetCursor();
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), sphereCastRadius);
            SortRaycastHitsByDistance(hits);
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();

                foreach (var raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        raycastable.GetCursorType().SetCursor();
                        return true;
                    }
                }
            }
            return false;
        }

        private RaycastHit[] SortRaycastHitsByDistance(RaycastHit[] hits)
        {
            float[] distances = new float[hits.Length];

            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits;
        }

        private bool InteractWithUI()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDraggingUI = false;
            }

            //if cursor is over the ui
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isDraggingUI = true;
                }
                cursors.UICursor.SetCursor();
                return true;
            }

            if (isDraggingUI) return true;

            return false;
        }

        private bool InteractWithMovement()
        {
            Vector3 moveLocation;
            bool hasHit = RaycastNavMesh(out moveLocation);
            if (hasHit)
            {
                if (!mover.CanMoveTo(moveLocation)) return false;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(moveLocation);
                }
                cursors.MovementCursor.SetCursor();
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 moveLocation)
        {
            moveLocation = new Vector3();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;

            moveLocation = navMeshHit.position;

            return true;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}