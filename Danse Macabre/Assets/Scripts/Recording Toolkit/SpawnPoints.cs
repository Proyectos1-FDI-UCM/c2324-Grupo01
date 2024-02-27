using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    #region references
    [SerializeField]
    private Recorder recordedData;
    [SerializeField]
    private GameObject circlePrefab;
    [SerializeField]
    private GameObject player;
    private float playerSpeed;
    [SerializeField]
    private GameObject startMusicPoint;
    private Transform startPointTransform;
    #endregion

    void Start()
    {
        playerSpeed = player.GetComponent<MovementComponent>().speed;
        startPointTransform = startMusicPoint.transform;

        SpawnRecordedPositions();
    }

    #region methods
    public void SpawnRecordedPositions()
    {
        foreach (var recordedEvent in recordedData.recordedEvents)
        {
            print(recordedEvent.positionX);
            Vector3 spawnPosition = new(startPointTransform.position.x + Math.Abs(recordedEvent.positionX - startPointTransform.position.x) * playerSpeed, -2.5f, 0f);

            Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
        }
    }
    #endregion
}