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
        Health target = null;
        float damage = 0f;

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            transform.LookAt(GetTargetLocation());
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
