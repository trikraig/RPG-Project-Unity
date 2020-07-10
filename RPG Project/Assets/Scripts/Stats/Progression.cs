using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            //Looking for matching passed stat and character class.
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                //If not desired class continue
                if (progressionClass.characterClass != characterClass) continue;
                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    //If not matching stat or if level doesnt exist for that stat continue
                    if (progressionStat.stat != stat || progressionStat.levels.Length < level) continue;
                    return progressionStat.levels[level - 1];
                }
            }
            return 0;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }

}