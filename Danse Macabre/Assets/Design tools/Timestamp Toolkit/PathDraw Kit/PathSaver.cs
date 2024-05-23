using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

public class PathSaver : MonoBehaviour
{
    #region parameters
    //public float playerSpeed;
    public float recordingTime = 2;
    private float recordingEnd;
    private bool recording = false;
    private bool firstClock;
    private bool pointRecorded = false;

    #endregion

    #region references
    private Transform playerTransform;
    #endregion

    #region properties
    private Vector3 transformInitialPosition; 
    #endregion

    #region data
    [SerializeField]
    public PathRecord pathData;
    #endregion

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        
        if (recording /*&& !pointRecorded*/)
        {
            SaveTrajectoryPoint();

            if (Time.time > recordingEnd) recording = false;
        }

        //pointRecorded = !pointRecorded;
    }

    #region methods
    private void SaveTrajectoryPoint()
    {

        if (firstClock)
        {
            transformInitialPosition = playerTransform.position;
            recordingEnd = Time.time + recordingTime;
            firstClock = false;
        }

        pathData.trajectoryPoints.Add(new PathRecord.trajectoryData
        {
            positionX = playerTransform.position.x - transformInitialPosition.x,
            positionY = playerTransform.position.y - transformInitialPosition.y,
        });

    }
    public void StartSaving()
    {
        recording = true;
        firstClock = true;
        SaveTrajectoryPoint();
    }
    #endregion

}
