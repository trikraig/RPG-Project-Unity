using RPG.Control;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] GameObject spawnPoint = null;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {

            if (sceneToLoad < 0)
            {
                Debug.LogError("scene to load is not set");
                yield break;
            }
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();

            //Remove control
            DisableControl();
            //Begin Fading out
            yield return fader.FadeOut(fadeOutTime);
            //Save Data to carry through
            wrapper.Save();
            //Load next scene
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            //Remove control from new player in new scene
            DisableControl();
            //Load current level
            wrapper.Load();
            //Find new portal to travel to
            Portal otherPortal = GetOtherPortal();
            //Trasport player to new level
            UpdatePlayer(otherPortal);
            //Create autosave for new position in level
            wrapper.Save();
            //Time to wait to allow everything to load
            yield return new WaitForSeconds(fadeWaitTime);
            //Fade in
            fader.FadeIn(fadeInTime);
            //Restore control
            EnableControl();
            //Destroy old portal
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.transform.position);
            player.transform.rotation = otherPortal.spawnPoint.transform.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }

            return null;
        }

        void DisableControl()
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl()
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;
        }
    }

}