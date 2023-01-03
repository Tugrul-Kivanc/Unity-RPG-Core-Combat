using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float pickupRespawnTime = 2f;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(pickupRespawnTime));
            }
        }

        IEnumerator HideForSeconds(float seconds)
        {
            SetPickupActive(false);
            yield return new WaitForSeconds(seconds);
            SetPickupActive(true);
        }

        void SetPickupActive(bool value)
        {
            GetComponent<Collider>().enabled = value;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(value);
            }
        }
    }

}