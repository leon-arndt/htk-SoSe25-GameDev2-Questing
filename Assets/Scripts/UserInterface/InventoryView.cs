using System;
using System.Collections.Generic;
using System.Linq;
using Logic;
using UnityEngine;

namespace UserInterface
{
    /// <summary>
    /// Show all the items (and their amounts) the player has collected using `ItemView`
    /// </summary>
    public class InventoryView : MonoBehaviour
    {
        private List<ItemView> _itemViews = new();

        private void Awake()
        {
            foreach (var itemView in GetComponentsInChildren<ItemView>())
            {
                _itemViews.Add(itemView);
            }
        }

        private void Update()
        {
            var itemDatas = GameState.Get<InventoryState>().Get().ToList();

            for (int i = 0; i < _itemViews.Count; i++)
            {
                // we check for each item view whether to show an item
                
                if (i < itemDatas.Count)
                {
                    // show the item
                    var item = itemDatas[i];
                    _itemViews[i].Show(item.Key, item.Value);
                }
                else
                {
                    // hide the item
                    _itemViews[i].Hide();
                }
            }
        }
    }
}