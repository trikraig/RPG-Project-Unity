using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health health = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas canvas = null;
        void Update()
        {
            float currentHealth = health.GetFraction();
            if (Mathf.Approximately(currentHealth, 0) || Mathf.Approximately(currentHealth, 1))
            {
                canvas.enabled = false;
                return;
            }
            canvas.enabled = true;
            foreground.localScale = new Vector3(currentHealth, 1, 1);
            //Use for float comparisons
        }
    }
}
