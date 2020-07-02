using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        SavingSystem ss;

        private void Start()
        {
            ss = GetComponent<SavingSystem>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Load() => ss.Load(defaultSaveFile);

        public void Save() => ss.Save(defaultSaveFile);
    }

}