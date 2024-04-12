using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Data", order = 10)]
public class LevelData : ScriptableObject
{
    [Header("Player speed")]
    public float playerSpeed = 10f;

}
