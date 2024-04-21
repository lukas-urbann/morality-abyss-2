using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Death : MonoBehaviour
    {
        private GameObject _blackFade;
        private Player.Controller _pController;

        void Start()
        {
            _pController = gameObject.GetComponent<Player.Controller>();
            _blackFade = GameObject.Find("BlackFade");
        }

        public void DeathEvent()
        {
            _blackFade.GetComponent<Animator>().SetBool("fade", true);
            _pController.isCrouching = true;
            _pController.properties.canMove = false;
            StartCoroutine(DeathTransition());
        }

        IEnumerator DeathTransition()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Death");
        }
    }
}