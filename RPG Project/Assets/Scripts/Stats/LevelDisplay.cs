using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats = null;
        Text textDisplay = null;
        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            textDisplay = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            textDisplay.text = string.Format("{0:0}", baseStats.GetLevel());
        }
    }
}
