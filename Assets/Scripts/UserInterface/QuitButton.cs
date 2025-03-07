using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class QuitButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(Application.Quit);
        }
    }
}