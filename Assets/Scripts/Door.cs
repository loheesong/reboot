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
        SceneManager.LoadScene("Level2");
    }

    public void Start()
    {
        // Get the Player Controller script
        pcontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    // When something collides, check that it is the player, and that they have the key. If not, do nothing
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the collider is a Player and they have collected the key, 
        if(other.gameObject.tag == "Player" && pcontroller.check_HasKey()){
            // TODO: probably move the scene getting to Start(), also this is a bit messy
            //string scenename = SceneManager.GetActiveScene().name;
            //string newSceneNum = (Int32.Parse(scenename[scenename.Length - 1].ToString()) + 1).ToString();
            int newSceneNum = SceneManager.GetActiveScene().buildIndex + 1;
            Debug.Log("Exit to next scene " + newSceneNum);
            SceneManager.LoadScene(newSceneNum);
        }
    }
}
