
using UnityEngine.SceneManagement;

namespace Logic
{
    public class GameState
    {
        public static void StartGame()
        {
            // clear the player inventory, set player life to 100, etc.
        }
    
        public static void EndGame()
        {
            SceneManager.LoadScene(2);
        }
    }
}
