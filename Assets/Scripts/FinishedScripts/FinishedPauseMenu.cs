using UnityEngine;
using UnityEngine.UI;

namespace FinishedScripts  
{  
    public class FinishedPauseMenu : MonoBehaviour  
    {  
        public GameObject pauseUi;  
        public GameObject settingsUi;  
        public Button settingsButton;  
        public Button continueButton;  
        public Button returnToPauseButton;  
         
        private void Awake()  
        {  
            settingsButton.onClick.AddListener(ActivateSettings);  
            continueButton.onClick.AddListener(Continue);  
            returnToPauseButton.onClick.AddListener(ActivatePauseMenu);  
              
            pauseUi.SetActive(false);  
            settingsUi.SetActive(false);  
        }  
  
        private void Update()  
        {  
            if (Input.GetKeyDown(KeyCode.Escape))  
            {  
                if (settingsUi.activeSelf)  
                {  
                    ActivatePauseMenu();  
                }  
                else if (pauseUi.activeSelf)  
                {  
                    Continue();  
                }  
                else  
                {  
                    ActivatePauseMenu();  
                }  
            }  
        }  
  
        private void Continue()  
        {  
            pauseUi.SetActive(false);  
            settingsUi.SetActive(false);  
            Time.timeScale = 1f;  
            Cursor.lockState = CursorLockMode.Locked;  
            Cursor.visible = false;  
        }  
  
        private void ActivateSettings()  
        {  
            pauseUi.SetActive(false);  
            settingsUi.SetActive(true);  
        }  
  
        private void ActivatePauseMenu()  
        {  
            settingsUi.SetActive(false);  
            pauseUi.SetActive(true);  
            Time.timeScale = 0f;  
            Cursor.lockState = CursorLockMode.None;  
            Cursor.visible = true;  
        }  
    }  
}