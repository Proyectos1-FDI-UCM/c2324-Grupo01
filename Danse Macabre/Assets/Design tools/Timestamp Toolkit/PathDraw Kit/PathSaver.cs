using UnityEngine;

/// <summary>
/// Tool for designing levels: saves path points from the player movement.
/// </summary>
public class PathSaver : MonoBehaviour
{
    #region parameters
    public float recordingTime = 2;
    private float recordingEnd;
    private bool recording = false;
    private bool firstClock;
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
        if (recording)
        {
            SaveTrajectoryPoint();
            if (Time.time > recordingEnd) recording = false;
        }
    }

    #region methods
    /// <summary>
    /// Saves trajectory points to a scriptable object.
    /// </summary>
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

    /// <summary>
    /// Called to start saving points. Usually aftes an input.
    /// </summary>
    public void StartSaving()
    {
        recording = true;
        firstClock = true;
        SaveTrajectoryPoint();
    }
    #endregion

}
