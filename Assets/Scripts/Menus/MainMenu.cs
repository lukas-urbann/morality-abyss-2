using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject settings, tutorial;
        
        public void Play()
        {
            SceneManager.LoadScene("Level1");
        }
        
        public void Tutorial()
        {
            tutorial.SetActive(true);
        }

        public void Settings()
        {
            if (!settings.activeSelf)
                settings.SetActive(true);
            else
                settings.SetActive(false);
        }
    
        public void Exit()
        {
            Application.Quit(69);
        }
    }
}