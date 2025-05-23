using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UserInterface
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private Button continueButton;
        [SerializeField] private InputActionReference pauseAction;
        
        private void Awake()
        {
            Continue();
            continueButton.onClick.AddListener(Continue);
        }

        private void Continue()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Pause()
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            if (pauseAction.action.WasPressedThisFrame())
            {
                if (pausePanel.activeSelf)
                {
                    // if the pause menu is active the game is paused: then we want to continue
                    Continue();
                }
                else
                {
                    // if the pause menu is not active the game is not paused: then we want to pause
                    Pause();
                }
            }
        }
    }
}