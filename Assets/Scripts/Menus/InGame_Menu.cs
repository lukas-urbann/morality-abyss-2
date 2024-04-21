using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class InGame_Menu : MonoBehaviour
    {
        private Player.Controller _pController;
        private GameObject _menu, _settings;
        public bool _pause = false, _setting = false;

        void Start()
        {
            _menu = GameObject.Find("Menu");
            _settings = GameObject.Find("Settings");
            _menu.SetActive(false);
            _settings.SetActive(false);
            _pController = GetComponent<Player.Controller>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }

        public void Settngs()
        {
            _setting = !_setting;

            if (_setting)
                _settings.SetActive(true);
            else
                _settings.SetActive(false);
        }

        public void Exit()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Pause()
        {
            _pause = !_pause;
            switch (_pause)
            {
                case true:
                    _pController.CursorControl(false);
                    _menu.SetActive(true);
                    Time.timeScale = 0;
                    _pController.properties.canMove = false;
                    break;
                case false:
                    _pController.CursorControl(true);
                    _menu.SetActive(false);
                    Time.timeScale = 1;
                    _pController.properties.canMove = true;
                    break;
            }
        }
    }
}