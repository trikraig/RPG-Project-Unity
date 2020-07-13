using RPG.Saving;
using System;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        //public delegate void ExperienceGainedDelegate();
        public event Action onExperienceGained;
        public void GainExperience(float experienceAmount)
        {
            experiencePoints += experienceAmount;
            //Calls subscribed methods in BaseStats
            onExperienceGained();
        }

        public float ExperiencePoints => experiencePoints;

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }

}