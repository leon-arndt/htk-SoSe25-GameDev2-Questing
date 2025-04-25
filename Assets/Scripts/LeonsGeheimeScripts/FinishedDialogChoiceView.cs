using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Cinematic
{
    public class FinishedDialogChoiceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI choiceText;
        [SerializeField] private TextMeshProUGUI hotKey;

        public Button Button => GetComponent<Button>();

        public void Set(string text, int hotkeyBinding)
        {
            choiceText.text = text;
            hotKey.text = hotkeyBinding.ToString();
        }

        public void Select()
        {
            GetComponent<Button>().Select();
        }
    }
}