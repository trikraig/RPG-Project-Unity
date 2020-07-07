using System;
using UnityEditor;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] Transform target = null;
        [SerializeField] float speed = 1;

        // Update is called once per frame
        void Update()
        {
            if (!target) { return; }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (!targetCapsule) { return target.position; }
            return target.position + (Vector3.up * targetCapsule.height / 2);
        }
    }
}
