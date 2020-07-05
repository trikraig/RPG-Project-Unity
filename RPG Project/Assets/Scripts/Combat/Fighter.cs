using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;
        
        Health target;
        Mover mover;
        Animator animator;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();

            SpawnWeapon();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            //No Target
            if (target == null) return;
            //Target is Dead
            if (target.IsDead()) return;
            //Engage Target

            if (!GetIsCurrentTargetInRange())
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            else
            {
                if (timeSinceLastAttack > timeBetweenAttacks)
                {
                    mover.Cancel();
                    AttackBehaviour();
                }
            }
        }

        private void SpawnWeapon()
        {
            if (weapon == null || handTransform == null)
            {
                return;
            }
            weapon.Spawn(handTransform, animator);

        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            TriggerAttackAnimation();
            timeSinceLastAttack = 0;
        }

        private void TriggerAttackAnimation()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        //Animation Event
        void Hit()
        {
            if (target == null) { return; }
            target.TakeDamage(weapon.GetDamage());
        }


        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetHealth = combatTarget.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
            mover.Cancel();
            StopAttackAnimation();
        }

        private void StopAttackAnimation()
        {
            animator.SetTrigger("stopAttack");
            animator.ResetTrigger("attack");
        }

        private bool GetIsCurrentTargetInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weapon.GetRange();
        }

        
    }
}

