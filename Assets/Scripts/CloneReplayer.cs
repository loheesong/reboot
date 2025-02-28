using System.Collections.Generic;
using UnityEngine;

public class CloneReplayer : MonoBehaviour {
    private List<PlayerController.RecordedFrame> replayData;
    private int frameIndex = 0;
    private Rigidbody2D rb;
    private float startTime;

    public LayerMask interactable;
    
    public void Initialize(List<PlayerController.RecordedFrame> data) {
        replayData = data;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // prevent interactions with other clones
        startTime = Time.time;
    }

    void FixedUpdate() {
        if (frameIndex >= replayData.Count) {
            Destroy(gameObject);
            return;
        }

        float elapsedTime = Time.time - startTime;

        while (frameIndex < replayData.Count && replayData[frameIndex].time <= elapsedTime) {
            frameIndex++;
        }

        if (frameIndex < replayData.Count) {
            rb.MovePosition(replayData[frameIndex].position);
            rb.rotation = replayData[frameIndex].rotation;
            rb.linearVelocity = replayData[frameIndex].velocity;
        }

        // need to fix later to interact with boxes 
        Collider2D[] detectedPlates = Physics2D.OverlapBoxAll(transform.position, rb.GetComponent<BoxCollider2D>().bounds.size, 0, interactable);
        foreach (Collider2D plate in detectedPlates)
        {
            plate.GetComponent<Button>().Activate(this.GetComponent<Collider2D>());
        }

    }
}
