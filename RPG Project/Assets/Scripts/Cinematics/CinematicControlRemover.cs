using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        PlayableDirector playableDirector;
        PlayerController playerController;
        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            playableDirector = GetComponent<PlayableDirector>();
            playerController = player.GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
        }
        private void OnDisable()
        {
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
        }
        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            playerController.enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            playerController.enabled = true;
        }
    }
}

