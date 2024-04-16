using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path Record", menuName = "Scriptable Objects/Path Record", order = 1)]
public class PathRecord : ScriptableObject
{
    [System.Serializable]
    public struct trajectoryData
    {
        public float positionX;
        public float positionY;
    }

    public List<trajectoryData> trajectoryPoints = new List<trajectoryData>();
}
