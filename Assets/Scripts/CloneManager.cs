using UnityEngine;
using System.Collections.Generic;

public class CloneManager : MonoBehaviour
{
    public GameObject clonePrefab;
    private PlayerController playerController;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        SpawnClones();
    }

    public void SpawnClones()
    {
        List<List<PlayerController.RecordedFrame>> allRecordings = playerController.GetAllRecordings();

        foreach (var recording in allRecordings)
        {
            // playerController.PrintRecording(recording);
            GameObject clone = Instantiate(clonePrefab, playerController.transform.position, playerController.transform.rotation);
            CloneReplayer replayer = clone.GetComponent<CloneReplayer>();
            replayer.Initialize(recording);
        }
    }
}
