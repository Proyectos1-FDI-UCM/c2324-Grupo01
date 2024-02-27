using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRecorder1 : MonoBehaviour
{
    [SerializeField]
    private Recorder recordedData; // Assign this in the Unity Editor
    private float timeSinceLastCollision = 1; // Indicates no collision has happened yet

    private void Update()
    {
        // If a collision has occurred
        if (timeSinceLastCollision >= 0)
        {
            timeSinceLastCollision += Time.deltaTime;

            // Record data when the X key is pressed
            if (Input.GetKeyDown(KeyCode.X))
            {
                recordedData.recordedEvents.Add(new Recorder.RecordedData
                {
                    positionX = transform.position.x,
                    timeSinceCollision = timeSinceLastCollision
                });
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StartRecord"))
        {
            timeSinceLastCollision = 0; // Reset the timer upon collision
        }
    }
}
