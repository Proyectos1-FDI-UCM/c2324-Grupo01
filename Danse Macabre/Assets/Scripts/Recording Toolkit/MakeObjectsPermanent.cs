#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

public class MakeObjectsPermanent : Editor
{
    [MenuItem("Tools/Recreate Spawned Objects")]
    private static void RecreateSpawnedObjects()
    {
        Recorder data = AssetDatabase.LoadAssetAtPath<Recorder>("Assets/Scripts/Recording Toolkit/R_Agudos.asset");

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scripts/Recording Toolkit/Point.prefab");

        MovementComponent movementComponent = FindAnyObjectByType<MovementComponent>();
        float playerSpeed = movementComponent.speed;

        GameObject startRecordCollider = GameObject.FindWithTag("StartRecord");
        float posX = startRecordCollider.transform.position.x;


        foreach (var recordedEvent in data.recordedEvents)
        {
            Vector3 spawnPosition = new(posX + Math.Abs(posX - recordedEvent.positionX) * playerSpeed, -2.5f, 0f);
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}
#endif
