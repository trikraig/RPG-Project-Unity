using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }
        
        public void TakeDamage(float damage)
        {
            print("take damage");
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                print("die");
                Die();
            }
        }

        private void Die()
        {
            if (!isDead)
            {
                print("is dead");
                GetComponent<Animator>().SetTrigger("die");
                isDead = true;
            }
        }
    }

}