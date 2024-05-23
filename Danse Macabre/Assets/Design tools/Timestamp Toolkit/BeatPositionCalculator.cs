using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatPositionCalculator : MonoBehaviour
{
    #region parameters
    public float playerSpeed;
    public float BPM;
    public float musicDurationSeconds;
    #endregion

    #region references
    [SerializeField]
    private GameObject startMusicPointObject;
    private Transform startMusicPointTransform;
    public GameObject BPMTimestampPrefab;
    public GameObject CustomBeatsTimestampPrefab;
    #endregion

    #region data
    [SerializeField]
    public TimestampContainer baseBPMData;
    [SerializeField]
    public TimestampContainer customBeatsData;
    #endregion

    #region methods
    public void CalculateBaseBeats()
    {
        startMusicPointTransform = startMusicPointObject.transform;

        float BPS = BPM/60;
        float periodSeconds = 1/BPS;
        float totalBeats = BPS*musicDurationSeconds;

        for (int i = 0; i < totalBeats; i++)
        {
            baseBPMData.timestamps.Add(new TimestampContainer.spaceTimeData
            {
                positionX = startMusicPointTransform.position.x + playerSpeed * periodSeconds * i,
                time = periodSeconds * i
            });
        }

        Debug.Log("BPM calculated and stored!");
    }
    public void CalculateCustomBeats()
    {
        startMusicPointTransform = startMusicPointObject.transform;

        for (int i = 0; i < customBeatsData.timestamps.Count; i++)
        {
            var data = customBeatsData.timestamps[i];
            data.positionX = startMusicPointTransform.position.x + playerSpeed * data.time;
            customBeatsData.timestamps[i] = data;
        }

        Debug.Log("Custom beats calculated and stored!");
    }
    #endregion

}
