using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] Text damageText = null;
        public void SetValue(float amount)
        {
            damageText.text = amount.ToString();
        }
    }

}