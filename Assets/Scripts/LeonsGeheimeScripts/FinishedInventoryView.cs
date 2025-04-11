using System.Collections.Generic;
using System.Linq;
using Logic;
using UnityEngine;

namespace LeonsGeheimeScripts
{
    public class FinishedInventoryView : MonoBehaviour
    {
        [SerializeField] private List<FinishedItemView> itemViews;

        private void Update()
        {
            var items = FinishedGameState.Get<FinishedInventoryState>().Get().ToList();
            
            for (var i = 0; i < itemViews.Count; i++)
            {
                if (i < items.Count)
                {
                    var item = items[i];
                    itemViews[i].gameObject.SetActive(true);
                    itemViews[i].Set(item.Key, item.Value);
                }
                else
                {
                    itemViews[i].gameObject.SetActive(false);
                }
            }
        }
    }
}