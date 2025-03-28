using TMPro;
using UnityEngine;

namespace LeonsGeheimeScripts
{
    public class FinishedInteractionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactionText;
        private static FinishedInteractionUI _instance;

        private void Awake()
        {
            _instance = this;
            _instance.interactionText.gameObject.SetActive(false);
        }

        public static void ShowPrompt(string prompt)
        {
            _instance.interactionText.text = prompt;
            _instance.interactionText.gameObject.SetActive(true);
        }

        public static void HidePrompt()
        {
            _instance.interactionText.gameObject.SetActive(false);
        }
    }
}