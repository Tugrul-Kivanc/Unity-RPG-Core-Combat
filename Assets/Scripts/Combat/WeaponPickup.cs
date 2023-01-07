using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] private Weapon weapon = null;
        [SerializeField] private float pickupRespawnTime = 2f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PickupWeapon(other.GetComponent<Fighter>());
            }
        }

        private void PickupWeapon(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
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
                PickupWeapon(callingController.GetComponent<Fighter>());
            }
            return true;
        }
    }

}