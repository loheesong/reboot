using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            // update player key flag 
            other.GetComponent<PlayerController>().CollectedKey();
            Destroy(gameObject);
        }
    }
}
