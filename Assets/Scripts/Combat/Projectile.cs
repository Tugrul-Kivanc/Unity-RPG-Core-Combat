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
        Collider targetCollider;

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            transform.LookAt(GetTargetLocation());
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        public void SetTarget(Health target)
        {
            this.target = target;
        }

        Vector3 GetTargetLocation()
        {
            targetCollider = target.GetComponent<Collider>();
            if (targetCollider == null) return target.transform.position;

            return targetCollider.bounds.center;
        }
    }
}
