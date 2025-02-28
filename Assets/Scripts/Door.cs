using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public void NextLevel() {
        Debug.Log("go next");
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().NextScene();
    }
}
