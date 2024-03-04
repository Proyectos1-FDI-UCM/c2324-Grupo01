using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Timestamp Container", menuName = "Scriptable Objects/Timestamp Container", order = 1)]
public class TimestampContainer : ScriptableObject
{
    [System.Serializable] // Make the struct visible in the Unity Editor.
    public struct spaceTimeData
    {
        public float positionX;
        public float time;
    }

    public List<spaceTimeData> timestamps = new List<spaceTimeData>();
}
