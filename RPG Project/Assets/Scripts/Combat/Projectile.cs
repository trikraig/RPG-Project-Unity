using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;

        Health target = null;
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

        
        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
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
                target.TakeDamage(damage);
                speed = 0;
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
