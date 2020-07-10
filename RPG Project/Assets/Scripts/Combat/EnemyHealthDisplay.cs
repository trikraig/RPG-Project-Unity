using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        // Start is called before the first frame update
        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (fighter.Target != null)
            {
                GetComponent<Text>().text = $"{fighter.Target.GetPercentage():0}%";
            }
            else
            {
                GetComponent<Text>().text = "N/A";
            }
        }
    }

}
