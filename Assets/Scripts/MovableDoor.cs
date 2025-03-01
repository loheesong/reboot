using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class MovableDoor : MonoBehaviour
{
    public Vector2 openPosition;
    [Tooltip("Set same as button groupID for button to control this door")]
    public int groupID;
    public float moveSpeed = 3f;
    private Vector2 closedPosition;
    private bool isOpening = false;
    
    void Start()
    {
        closedPosition = transform.position;
    }
    public void OpenDoor() {
        if (!isOpening) {
            isOpening = true;
            StopAllCoroutines();
            StartCoroutine(MoveDoor(openPosition));
        }
    }

    public void CloseDoor() {
        if (isOpening) {
            isOpening = false;
            StopAllCoroutines();
            StartCoroutine(MoveDoor(closedPosition));
        }
    }

    private IEnumerator MoveDoor(Vector2 openPosition) {
        while ((Vector2)transform.position != openPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, openPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }
    }
}
