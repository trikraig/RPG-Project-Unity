using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience = null;
        Text textDisplay = null;
        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            textDisplay = GetComponent<Text>();
        }

        private void Update()
        {
            textDisplay.text = string.Format("{0:0}", experience.ExperiencePoints);
        }
    }

}
