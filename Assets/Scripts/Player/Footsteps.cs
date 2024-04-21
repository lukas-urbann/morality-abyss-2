using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Footsteps : MonoBehaviour
    {
        private Controller _controller;
        public AudioClip footstep, runFootstep, jumpFootstep;

        private void Start()
        {
            _controller = GetComponent<Controller>();
        }
        
        public void FootstepAudio(string type)
        {
            _controller.properties.AudioSource.pitch = Random.Range(1f, 1.5f);
            switch (type)
            {
                case "walk":
                    _controller.properties.AudioSource.clip = footstep;
                    break;
                case "run":
                    _controller.properties.AudioSource.clip = runFootstep;
                    break;
            }
            _controller.properties.AudioSource.Play();
        }
        
        public void JumpAudio()
        {
            _controller.properties.AudioSource.clip = jumpFootstep;
            _controller.properties.AudioSource.Play();
        }
    }
}