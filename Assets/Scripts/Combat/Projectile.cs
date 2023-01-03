using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 2f;
        [SerializeField] bool isHoming = false;
        Health target = null;
        float damage = 0f;
        bool initialLook = true;

        void Start()
        {
            transform.LookAt(GetTargetLocation());
        }

        void Update()
        {
            if (target == null) return;

            if (isHoming)
            {
                transform.LookAt(GetTargetLocation());
            }
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        Vector3 GetTargetLocation()
        {
            Collider targetCollider = target.GetComponent<Collider>();
            if (targetCollider == null) return target.transform.position;

            return targetCollider.bounds.center;
        }

        void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);

            if (other.GetComponent<Health>() != target) return;

            target.TakeDamage(damage);
        }
    }
}
