using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        Weapon currentWeapon = null;

        Health target;
        Mover mover;
        Animator animator;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();

            EquipWeapon(defaultWeapon);
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

        public void EquipWeapon(Weapon weapon)
        {
            if (weapon == null || rightHandTransform == null || leftHandTransform == null) { return; }
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
            currentWeapon = weapon;

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
            if (!target) { return; }
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetDamage());
            }
        }
        //Animation Event
        void Shoot()
        {
            Hit();
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
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }


    }
}

