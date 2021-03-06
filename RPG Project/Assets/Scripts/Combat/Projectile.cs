﻿using RPG.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] bool isAreaOfEffect = false;
        [SerializeField] float areaOfEffectRange = 1f;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;
        [SerializeField] UnityEvent onHit;

        Health target = null;
        GameObject instigator = null;
        float damage = 0;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) { return; }
            //Homing Arrows
            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            //Basic Arrow Movement
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }


        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;
            //Max life time of projectile
            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (!targetCapsule) { return target.transform.position; }
            return target.transform.position + (Vector3.up * targetCapsule.height / 2);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() == target && !target.IsDead())
            {
                if (isAreaOfEffect)
                {
                    RaycastHit[] hits = Physics.SphereCastAll(GetAimLocation(), areaOfEffectRange, Vector3.up, 0);
                    foreach (RaycastHit hit in hits)
                    {
                        Health nearbyEnemy = hit.collider.GetComponent<Health>();
                        if (nearbyEnemy == null) { continue; }
                        nearbyEnemy.TakeDamage(instigator, damage);
                    }
                }
                else
                {

                    target.TakeDamage(instigator, damage);
                }

                speed = 0;
                onHit.Invoke();
                if (hitEffect) { Instantiate(hitEffect, GetAimLocation(), transform.rotation); }

                foreach (GameObject toDestroy in destroyOnHit)
                {
                    Destroy(toDestroy);
                }

                Destroy(gameObject, lifeAfterImpact);
            }
        }
    }
}
