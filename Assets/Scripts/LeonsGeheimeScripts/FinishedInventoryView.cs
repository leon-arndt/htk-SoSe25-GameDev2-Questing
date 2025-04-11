using Logic;
using UnityEngine;

namespace LeonsGeheimeScripts
{
    public class FinishedInventoryView : MonoBehaviour
    {
        private void Update()
        {
            var items = FinishedGameState.Get<FinishedInventoryState>().Get();
        }
    }
}