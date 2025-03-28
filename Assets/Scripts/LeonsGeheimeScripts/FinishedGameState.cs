using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Logic
{
    public class FinishedGameState
    {
        private static readonly List<SaveState> _gameStates = new List<SaveState>()
        {
            new FinishedInventoryState(),
        };
        public static void StartGame()
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
    }
}