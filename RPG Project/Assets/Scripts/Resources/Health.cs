using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;
        float maxHealth;
        bool isDead = false;

        private void Start()
        {
            maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            if (healthPoints < 0)
            {
                 healthPoints = maxHealth;
            }
        }

        public bool IsDead() => isDead;

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
                
            }

        }

        private void AwardExperience(GameObject instigator)
        {
            if (instigator.GetComponent<Experience>() == null) return;
            instigator.GetComponent<Experience>().GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return healthPoints / maxHealth * 100;
        }

        private void Die()
        {
            if (!isDead)
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
        }

        public object CaptureState()
        {
            return healthPoints;
        }
        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints == 0)
            {
                Die();
            }
        }
    }

}