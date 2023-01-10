using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] private WeaponConfig weapon = null;
        [SerializeField] private float pickupRespawnTime = 2f;
        [SerializeField] private float healthToRestore = 0f;
        [SerializeField] private CursorType pickupCursor;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Pickup(other.gameObject);
            }
        }

        private void Pickup(GameObject objectToPickup)
        {
            if (weapon != null)
            {
                objectToPickup.GetComponent<Fighter>().EquipWeapon(weapon);
            }

            if (healthToRestore > 0)
            {
                objectToPickup.GetComponent<Health>().Heal(healthToRestore);
            }
            StartCoroutine(HideForSeconds(pickupRespawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            SetPickupActive(false);
            yield return new WaitForSeconds(seconds);
            SetPickupActive(true);
        }

        private void SetPickupActive(bool value)
        {
            GetComponent<Collider>().enabled = value;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(value);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return pickupCursor;
        }
    }

}