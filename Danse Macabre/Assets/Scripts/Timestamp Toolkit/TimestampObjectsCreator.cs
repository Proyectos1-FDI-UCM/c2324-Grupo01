#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

public class TimestampObjectsCreator : Editor
{
    [MenuItem("Tools/Create Timestamp Objects")]
    private static void CreateTimestampObjects()
    {
        // Cambiar esto "Assets/Scripts/Timestamp Toolkit/CAMBIAR.asset":
        TimestampContainer data = AssetDatabase.LoadAssetAtPath<TimestampContainer>("Assets/Scripts/Timestamp Toolkit/Record1.asset");

        GameObject stamp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scripts/Timestamp Toolkit/Stamp.prefab");

        MovementComponent movementComponent = FindAnyObjectByType<MovementComponent>();
        float playerSpeed = movementComponent.speed;

        GameObject startRecordCollider = GameObject.FindWithTag("Start Music");
        float posX = startRecordCollider.transform.position.x;


        foreach (var timestamp in data.timestamps)
        {
            Vector3 position = new(timestamp.positionX, 0f, 0f);
            Instantiate(stamp, position, Quaternion.identity);
        }
    }
}
#endif
