using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public void NextLevel() {
        Debug.Log("go nexg");
        SceneManager.LoadScene("Level2");
    }
}
