using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    bool isFloor(GameObject gameObject) {
        return gameObject.layer == LayerMask.NameToLayer("Floor");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponentInParent<PlayerController>().canJump = isFloor(collision.gameObject);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //GetComponentInParent<PlayerController>().canJump = false;
    }
}
