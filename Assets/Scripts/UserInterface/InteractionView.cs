using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class InteractionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactionText;
        private static InteractionView _instance;

        private void Awake()
        {
            // dies ist die "Singleton" instance für diese Klasse - es gibt diese GameObject auf jeden Fall nur 1-mal
            _instance = this;
            HidePrompt();
        }

        public static void ShowPrompt(string prompt)
        {
            // show the interaction text
            _instance.interactionText.text = prompt;
            _instance.interactionText.gameObject.SetActive(true);
        }

        public static void HidePrompt()
        {
            // hide the interaction text
            _instance.interactionText.gameObject.SetActive(false);
        }
    }
}