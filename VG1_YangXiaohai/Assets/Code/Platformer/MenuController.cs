using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;

        //Outlets
        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject levelMenu;


        //Methods
        private void Awake()
        {
            instance = this;
            Hide();
        }

        void SwitchMenu(GameObject someMenu)
        {
            //Clean-up Menus
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            levelMenu.SetActive(false);

            //Turn on request menu
            someMenu.SetActive(true);
        }

        public void ShowMainMenu()
        {
            SwitchMenu(mainMenu);
        }

        public void ShowOptionsMenu()
        {
            SwitchMenu(optionsMenu);
        }

        public void ShowLevelMenu()
        {
            SwitchMenu(levelMenu);
        }

        public void Show()
        {
            ShowMainMenu();
            gameObject.SetActive(true);
            Time.timeScale = 0;
            PlayerController.instance.isPaused = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            if(PlayerController.instance != null)
            {
                PlayerController.instance.isPaused = false;
            }
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}