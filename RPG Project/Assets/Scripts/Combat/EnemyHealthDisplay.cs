using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        Text textDisplay;
        // Start is called before the first frame update
        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            textDisplay = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            if (fighter.Target != null)
            {
               textDisplay.text = string.Format("{0:0}/{1:0}", fighter.Target.GetHealthPoints(), fighter.Target.GetMaxHealthPoints());
            }
            else
            {
               textDisplay.text = "N/A";
            }
        }
    }

}
