using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeetCollider : MonoBehaviour
{
    bool isFloor(GameObject gameObject) {
        return gameObject.layer == LayerMask.NameToLayer("Floor");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check to see if player hit the floor so they can get jump perms back
        GetComponentInParent<PlayerController>().canJump = isFloor(collision.gameObject);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        GetComponentInParent<PlayerController>().canJump = false;
    }
}
