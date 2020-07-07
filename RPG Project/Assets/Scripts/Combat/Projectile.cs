using RPG.Core;
using System;
using UnityEditor;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] float speed = 1;
        Health target = null;

        // Update is called once per frame
        void Update()
        {
            if (!target) { return; }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget (Health target)
        {
            this.target = target;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (!targetCapsule) { return target.transform.position; }
            return target.transform.position + (Vector3.up * targetCapsule.height / 2);
        }
    }
}
