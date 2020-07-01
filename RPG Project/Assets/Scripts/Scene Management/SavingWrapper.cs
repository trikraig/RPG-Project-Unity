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

        private void Load() => ss.Load(defaultSaveFile);

        private void Save() => ss.Save(defaultSaveFile);
    }

}