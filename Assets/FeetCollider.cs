using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeetCollider : MonoBehaviour
{
    bool isFloor(GameObject gameObject) {
        //return gameObject.layer == LayerMask.NameToLayer("Floor");
        Debug.Log("jump");
        return gameObject.tag == "Floor";
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check to see if player hit the floor so they can get jump perms back
        GetComponentInParent<PlayerController>().canJump = isFloor(collision.gameObject);
        // Check to see if door exit, leave scene
        if (collision.gameObject.tag == "Exit") {
            string scenename = SceneManager.GetActiveScene().name;
            string newSceneNum = (((int) scenename[scenename.Length - 1]) + 1).ToString();
            Debug.Log("Exit to next scene " + newSceneNum);
            SceneManager.LoadScene("Level" + newSceneNum);}
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //GetComponentInParent<PlayerController>().canJump = false;
    }
}
