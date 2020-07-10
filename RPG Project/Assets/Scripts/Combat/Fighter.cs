using RPG.Core;
using RPG.Movement;
using RPG.Resources;
using RPG.Saving;
using UnityEngine;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction, ISaveable
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

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            
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
        
        public Health Target => target;

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
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, gameObject, target);
            }
            else
            {
                target.TakeDamage(gameObject, currentWeapon.GetDamage());
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

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            EquipWeapon(UnityEngine.Resources.Load<Weapon>((string)state));
        }
    }
}

