using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    private Transform _dummyTransform;
    // [SerializeField]
    // private LevelDataLoader _levelDataLoader;
    [SerializeField]
    private PathRecord _jumpPathRecord;
    [SerializeField]
    private PathRecord _stompPathRecord;
    [SerializeField]
    private PathRecord _trampolinePathRecord;


    private float detectionDistance = 0.51499f;
    [SerializeField]
    private LayerMask layers;
    // [SerializeField]
    // private LayerMask trampolineLayer;
    // [SerializeField]
    // private LayerMask enemyLayer;
    public bool JumpPath = false;
    public bool StompPath = false;
    public bool TrampolinePath = false;
    public bool detectCollisionBellow = false;
    //public bool detectTrampoline = false;
    //public bool detectEnemyBellow = true;


    private void DrawPath(PathRecord pathRecord)
    {
        _dummyTransform = transform;

        Vector3 startPosition = _dummyTransform.position;
        Vector3 previousPoint = startPosition;

        foreach (var trajectoryPoint in pathRecord.trajectoryPoints)
        {
            float dx = trajectoryPoint.positionX + startPosition.x;
            float dy = trajectoryPoint.positionY + startPosition.y;
            Vector3 nextPoint = new Vector3(dx, dy, 0);

            if (math.abs(previousPoint.x - nextPoint.x) > 3) previousPoint = startPosition;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(previousPoint, nextPoint);

            previousPoint = nextPoint;
        }
        
    }

    void OnDrawGizmos()
    {
        if (JumpPath){

            DrawPath(_jumpPathRecord);
        }

        if (StompPath)
        {
            DrawPath(_stompPathRecord);      
        }

        if (TrampolinePath)
        {
            DrawPath(_trampolinePathRecord);
        }


        // DETECTION
        if (detectCollisionBellow)
        {
            RaycastHit2D hit = Physics2D.Raycast(_dummyTransform.position, Vector2.down, detectionDistance, layers);
            Gizmos.color = Color.red;
            if (hit.collider != null) Gizmos.DrawSphere(_dummyTransform.position, 0.2f);
        }
    }
}


