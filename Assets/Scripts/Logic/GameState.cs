using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic
{
    /// <summary>
    /// The game state has all the save states
    /// </summary>
    public class GameState : MonoBehaviour
    {
        /// <summary>
        /// All the save states in the game are listed here: inventory, quests
        /// </summary>
        private static readonly List<SaveState> _saveStates = new List<SaveState>()
        {
            new InventoryState(),
            new QuestsState(),
            // ..., for example QuestState, NpcState, BossState
        };

        public void Awake()
        {
            foreach (var state in _saveStates)
            {
                state.OnStartGame();
            }
        }

        public static void EndGame()
        {
            foreach (var state in _saveStates)
            {
                state.OnEndGame();
            }

            // also load the win screen scene
            SceneManager.LoadScene(2);
        }

        // get a specific type of save state, for example the InventoryState
        // T means Type, it's a generic type
        public static T Get<T>() where T : SaveState
        {
            foreach (var saveState in _saveStates)
            {
                if (saveState is T t)
                {
                    // this is the correct type, so we return it
                    return t;
                }
            }

            // if we reach this point, we did not find the save state and have a problem
            throw new System.Exception($"No save state of type {typeof(T)} found.");
        }
    }
}