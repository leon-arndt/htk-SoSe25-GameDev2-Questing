using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic
{
    public class FinishedGameState : MonoBehaviour
    {
        private static readonly List<SaveState> _gameStates = new List<SaveState>()
        {
            new FinishedInventoryState(),
        };

        public void Awake()
        {
            foreach (var state in _gameStates)
            {
                state.OnStartGame();
            }
        }

        public static void EndGame()
        {
            foreach (var state in _gameStates)
            {
                state.OnEndGame();
            }

            SceneManager.LoadScene(2);
        }
        
        public static T Get<T> () where T : SaveState
        {
            foreach (var saveState in _gameStates)
            {
                if (saveState is T t)
                {
                    return t;
                }
            }
            
            throw new System.Exception($"No save state of type {typeof(T)} found.");
        }
    }
}