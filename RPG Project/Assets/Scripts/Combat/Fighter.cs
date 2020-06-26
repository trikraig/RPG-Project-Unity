using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Health target = null;

        float timeSinceLastAttack = 0;
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
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                if (timeSinceLastAttack > timeBetweenAttacks)
                {
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();
                }
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            TriggerAttackAnimation();
            timeSinceLastAttack = 0;
        }

        private void TriggerAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation Event
        void Hit()
        {
            if (target == null) { return; }
            target.TakeDamage(weaponDamage);
        }


        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetHealth = combatTarget.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead();
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
            StopAttackAnimation();
        }

        private void StopAttackAnimation()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            GetComponent<Animator>().ResetTrigger("attack");
        }

        private bool GetIsCurrentTargetInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }
    }
}

