using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private int currentLevel = 1;
    private int maxLevel;
    #region Unity_functions
    private void Awake() {
        maxLevel = SceneManager.sceneCountInBuildSettings;
        Debug.Log("gamemanager");
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_transitions
    public void StartGame() {
        SceneManager.LoadScene($"Level{currentLevel}");
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void NextScene() {
        currentLevel++;

        if (currentLevel <= maxLevel) {
            SceneManager.LoadScene($"Level{currentLevel}");
        }
    }

    public void ReloadCurrentLevel() {
        Debug.Log("asd");
        SceneManager.LoadScene($"Level{currentLevel}");
    }
    #endregion
}
