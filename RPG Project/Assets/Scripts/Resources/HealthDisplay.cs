using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        Text textDisplay = null;
        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            textDisplay = GetComponent<Text>();
        }
        private void Update()
        {
            textDisplay.text = string.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }

}