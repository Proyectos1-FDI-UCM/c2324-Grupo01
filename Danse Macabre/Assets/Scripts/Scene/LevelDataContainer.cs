using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data Container", menuName = "Scriptable Objects/Level Data Container", order = 1)]
public class LevelDataContainer : ScriptableObject
{
    [System.Serializable]
    public struct LevelData
    {
        public string sceneName;
        public float playerSpeed;
    }

    public List<LevelData> Levels = new List<LevelData>();
}
