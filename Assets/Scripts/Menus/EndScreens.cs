using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class EndScreens : MonoBehaviour
    {
        public void LoadMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}