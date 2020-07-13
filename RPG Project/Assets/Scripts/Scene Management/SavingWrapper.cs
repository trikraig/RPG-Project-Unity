using RPG.Saving;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] float fadeInTime = 0.5f;
        const string defaultSaveFile = "save";
        SavingSystem savingSystem = null;
        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }
        private void Start()
        {
            StartCoroutine(LoadLastScene());
        }
        IEnumerator LoadLastScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return savingSystem.LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);

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
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public void Load() => savingSystem.Load(defaultSaveFile);

        public void Save() => savingSystem.Save(defaultSaveFile);

        public void Delete()
        {
            savingSystem.Delete(defaultSaveFile);
        }
    }

}