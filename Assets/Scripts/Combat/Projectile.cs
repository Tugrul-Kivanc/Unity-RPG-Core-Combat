using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float maxLifeTime = 5f;
        [SerializeField] private float lifeAfterImpact = 0.1f;
        [SerializeField] private bool isHoming = false;
        [SerializeField] private GameObject hitFX = null;
        [SerializeField] private GameObject[] objectsToDestroyOnHit = null;
        private Health target = null;
        private GameObject instigator = null;
        private float damage = 0f;

        private void Start()
        {
            transform.LookAt(GetTargetLocation());
        }

        private void Update()
        {
            if (target == null) return;

            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetTargetLocation());
            }
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetTargetLocation()
        {
            Collider targetCollider = target.GetComponent<Collider>();
            if (targetCollider == null) return target.transform.position;

            return targetCollider.bounds.center;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;

            target.TakeDamage(instigator, damage);
            speed = 0f;

            if (hitFX != null)
            {
                Instantiate(hitFX, transform.position, transform.rotation);
            }

            foreach (var objectToDestroy in objectsToDestroyOnHit)
            {
                Destroy(objectToDestroy);
            }
            Destroy(gameObject, lifeAfterImpact);
        }
    }
}
