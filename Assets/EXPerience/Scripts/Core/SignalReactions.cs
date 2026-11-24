using UnityEngine;
using Cinemachine;

namespace EXPerience
{
    public class SignalReactions : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera playerVC;
        [SerializeField] AudioSource music;
        [SerializeField] MonoBehaviour[] initialScripts;

        public void StartCutscene()
        {
            StaticInputManager.input.Disable();
            music.Play();
        }

        public void AllowMovement()
        {
            playerVC.Priority = 0;
            StaticInputManager.input.Enable();
        }

        public void StartBoss()
        {
            foreach (var script in initialScripts)
                script.enabled = true;
        }
    }
}