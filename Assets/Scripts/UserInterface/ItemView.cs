using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    /// <summary>
    /// This class will show an item icon along with its amount text
    /// </summary>
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemAmountText;

        public void Show(ItemType type, uint amount)
        {
            gameObject.SetActive(true);
            itemIcon.sprite = type.icon;
            itemAmountText.text = amount.ToString();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}