using System;
using RPG.Combat;
using RPG.Attributes;
using RPG.Movement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] GameObject cursorsPrefab;
        private Health health;
        private Cursors cursors;
        private void Awake()
        {
            health = GetComponent<Health>();
            cursors = cursorsPrefab.GetComponent<Cursors>();
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
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
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
            //if cursor is over the ui
            if (EventSystem.current.IsPointerOverGameObject())
            {
                cursors.UICursor.SetCursor();
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                cursors.MovementCursor.SetCursor();
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}