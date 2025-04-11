using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeonsGeheimeScripts
{
    public class FinishedItemView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI amountText;
        
        public void Set(FinishedItemType itemType, uint amount)
        {
            icon.sprite = itemType.ItemSprite;
            amountText.text = amount.ToString();
        }
    }
}