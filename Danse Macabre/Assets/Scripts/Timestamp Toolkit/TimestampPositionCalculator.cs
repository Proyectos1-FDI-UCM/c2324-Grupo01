using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimestampPositionCalculator : MonoBehaviour
{
    #region parameters
    //[SerializeField]
    //private float bpm = 139;
    #endregion

    #region references
    // Game Manager
    [SerializeField]
    private GameObject manager;
    private TempoManager tempo;

    // Music Manager
    [SerializeField]
    private GameObject musicManager;
    private MusicManager musicManagerComponent;

    // Player
    [SerializeField]
    private GameObject player;
    private MovementComponent playerMovement;
    private float playerSpeed;

    // Start Music Reference Point
    [SerializeField]
    private GameObject startMusicPoint;
    private Transform startPointTransform;

    // Data
    [SerializeField]
    private TimestampContainer spaceTimeData;
    [SerializeField]
    private TimestampContainer beatsData;
    #endregion

    #region properties
    //private float timeSinceLastCollision = -1; // Indicates no collision has happened yet
    #endregion

    private void Start()
    {
        playerMovement = GetComponent<MovementComponent>();
        playerSpeed = playerMovement.speed;

        musicManagerComponent = musicManager.GetComponent<MusicManager>();
        startPointTransform = startMusicPoint.transform;

        tempo = manager.GetComponent<TempoManager>();

        // For main beats calculations:
        //float bps = tempo.bpm/60;
        //float period = 1/bps;

        // duration p1 = 28.705
        // duration p2 = 
        float musicDuration = 29;
        float totalBeats = (139*musicDuration)/60;


        // For marking beats:
        // for (int i = 0; i < totalBeats; i++)
        // {
        //     beatsData.timestamps.Add(new TimestampContainer.spaceTimeData
        //     {
        //         positionX = startPointTransform.position.x + playerSpeed * period * i,
        //         time = period*i
        //     });
        // }


        // For calculating/translating time stamps to space f(startPoint, speed, time):
        // for (int i = 0; i < spaceTimeData.timestamps.Count; i++)
        // {
        //     var data = spaceTimeData.timestamps[i];
        //     data.positionX = startPointTransform.position.x + playerSpeed * data.time;
        //     spaceTimeData.timestamps[i] = data;
        // }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Start Music"))
        {
            print("time: " + Time.time);
            musicManagerComponent.PlayMusic();
            //timeSinceLastCollision = 0;
        }
    }
}
