using UnityEngine;

namespace LeonsGeheimeScripts
{
   public abstract class FinishedInteractable : MonoBehaviour
   {
      public abstract void Interact(Transform interactor);
   }
}
