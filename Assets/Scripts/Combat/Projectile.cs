using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float speed = 2f;

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            transform.LookAt(GetTargetLocation());
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        Vector3 GetTargetLocation()
        {
            CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
            if (targetCollider == null) return target.position;

            return target.position + targetCollider.height * 0.5f * Vector3.up;
        }
    }
}
