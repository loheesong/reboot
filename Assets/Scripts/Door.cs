using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    # region Private Vars
    private PlayerController pcontroller;
    # endregion

    public void NextLevel() {
        Debug.Log("go next");
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().NextScene();
    }
    void Start()
    {
        // Get the Player Controller script
        Debug.Log("init door");
        pcontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Debug.Log(pcontroller.check_HasKey());
    }


    // When something collides, check that it is the player, and that they have the key. If not, do nothing
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Inefficient to have this line here, but may fix issue?
        pcontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        // If the collider is a Player and they have collected the key, 
        if(other.CompareTag("Player") && pcontroller.check_HasKey()){
            // TODO: probably move the scene getting to Start(), also this is a bit messy
            //string scenename = SceneManager.GetActiveScene().name;
            //string newSceneNum = (Int32.Parse(scenename[scenename.Length - 1].ToString()) + 1).ToString();
            int newSceneNum = SceneManager.GetActiveScene().buildIndex + 1;
            Debug.Log("Exit to next scene " + newSceneNum);
            SceneManager.LoadScene(newSceneNum);
        }
    }
}
