using System.Linq;
using UnityEngine;

public class Button : MonoBehaviour
{
    public int groupID; // id to control which doors get affected
    private int objectsOnPlate = 0;
    private MovableDoor[] movableDoors;

    void Start() {
        movableDoors = FindObjectsByType<MovableDoor>(FindObjectsSortMode.None).Where(d => d.groupID == groupID).ToArray();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Activate(collision);
    }

    public void Activate(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            objectsOnPlate++;
            foreach (MovableDoor door in movableDoors) {
                door.OpenDoor();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            objectsOnPlate--;
            if (objectsOnPlate == 0) {
                foreach (MovableDoor door in movableDoors) {
                    door.CloseDoor();
                }
            }
        }
    }
}
