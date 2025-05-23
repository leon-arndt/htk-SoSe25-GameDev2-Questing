using System;
using UnityEngine;
using UnityEngine.UI;

namespace LeonsGeheimeScripts
{
    public class FinishedSimplePauseMenu : MonoBehaviour
    {
        public GameObject pausePanel;
        public Button continueButton;  

        private void Awake()
        {
            Continue();
            continueButton.onClick.AddListener(Continue);  
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))  
            {  
                if (pausePanel.activeSelf)  
                {  
                    Continue();  
                }  
                else  
                {  
                    Pause();  
                }  
            }
        }

        private void Pause()
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;  
            Cursor.lockState = CursorLockMode.None;  
            Cursor.visible = true; 
            continueButton.Select();
        }

        private void Continue()
        {
            pausePanel.SetActive(false);  
            Time.timeScale = 1f;  
            Cursor.lockState = CursorLockMode.Locked;  
            Cursor.visible = false;  
        }
    }
}