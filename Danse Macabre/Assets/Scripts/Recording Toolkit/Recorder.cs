using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recorder", menuName = "Scriptable Objects/Recorder", order = 1)]
public class Recorder : ScriptableObject
{
    [System.Serializable] // Make the struct visible in the Unity Editor.
    public struct RecordedData
    {
        public float positionX;
        public float timeSinceCollision;
    }

    public List<RecordedData> recordedEvents = new List<RecordedData>();
}
