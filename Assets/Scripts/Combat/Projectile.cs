using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 2f;
        [SerializeField] float maxLifeTime = 5f;
        [SerializeField] float lifeAfterImpact = 0.1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitFX = null;
        [SerializeField] GameObject[] objectsToDestroyOnHit = null;
        Health target = null;
        float damage = 0f;

        void Start()
        {
            transform.LookAt(GetTargetLocation());
        }

        void Update()
        {
            if (target == null) return;

            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetTargetLocation());
            }
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
        }

        Vector3 GetTargetLocation()
        {
            Collider targetCollider = target.GetComponent<Collider>();
            if (targetCollider == null) return target.transform.position;

            return targetCollider.bounds.center;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;

            target.TakeDamage(damage);
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
