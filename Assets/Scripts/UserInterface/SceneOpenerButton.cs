using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneOpenerButton : MonoBehaviour
{
    // welcher Nummer hat die Szene in den Build-Settings (0, 1, 2, 3, ...)
    public int sceneIndex;
    
    // Start passiert einmal wenn dieses GameObject erstellt wird
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenScene);
    }

    private void OpenScene()
    {
        // z.B. in der MainMenu scene wird der Button angeclickt um Scene-build-index 1 zu öffnen (Game.unity)
        SceneManager.LoadScene(sceneIndex);
    }
}
