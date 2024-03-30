using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPathDrawer : MonoBehaviour
{
    private Rigidbody2D _rb;
    private ActionComponent _actionComponent;
    [SerializeField]
    private LevelDataLoader _levelDataLoader;

    public float jumpSpeed;
    public float trampolineJumpSpeed;
    public float horizontalVelocity;
    private float gravityUpwards;
    private float gravityDownwards;
    private float gravity;
    private int numPoints = 250;
    private int numPointsDown = 150;
    private float timeBetweenPoints = 0.1f;

    public bool activateJumpPath = true;
    public bool activateStompPath = true;
    public bool activateTrampolinePath = true;
    public bool useCustomHorizontalVelocity = false;
    public bool useCustomJumpSpeed = false;

    void OnDrawGizmos()
    {
        _actionComponent = GetComponent<ActionComponent>();
        _rb = GetComponent<Rigidbody2D>();

        if (!useCustomHorizontalVelocity) horizontalVelocity = _levelDataLoader.GetCurrentScenePlayerSpeed();
        if (!useCustomJumpSpeed) jumpSpeed = _actionComponent.jumpSpeed;

        gravityUpwards = Physics2D.gravity.y * _actionComponent.originalGravityScale; // for vel.y > 0
        gravityDownwards = gravityUpwards * _actionComponent.gravityFactor; // for vel.y < 0
        
        if (activateJumpPath)
        {
            Vector3 startPosition = transform.position;
            Vector3 previousPoint = startPosition;
            float totalTime = 0f;
            float timeBetweenPoints = 0.05f;
            gravity = gravityUpwards;

            bool goingUp = true;
            for (int i = 0; i < numPoints && goingUp; i++)
            {
                totalTime += timeBetweenPoints;
                float dx = horizontalVelocity * totalTime;
                float dy = (jumpSpeed * totalTime) + (0.5f * gravity * Mathf.Pow(totalTime, 2));
                Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

                if (nextPoint.y < previousPoint.y) goingUp = false;
                //if (nextPoint.y < previousPoint.y) gravity = gravityDownwards;
                // Draw the line between the previous point and the next point
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(previousPoint, nextPoint);
                previousPoint = nextPoint;
            }

            gravity = gravityDownwards;
            startPosition = previousPoint;
            totalTime = 0;

            for (int i = 0; i < numPointsDown; i++)
            {
                totalTime += timeBetweenPoints;
                float dx = horizontalVelocity * totalTime;
                float dy = 0.5f * gravity * Mathf.Pow(totalTime, 2);
                //print("dy: " + dy);
                Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

                // Draw the line between the previous point and the next point
                Gizmos.color = Color.red;
                Gizmos.DrawLine(previousPoint, nextPoint);
                previousPoint = nextPoint;
            }
        }

        if (activateStompPath)
        {
            Vector3 startPosition = transform.position;
            Vector3 previousPoint = startPosition;
            float totalTime = 0f;
            gravity = gravityDownwards;
            float downwardSpeed = - _actionComponent.stompDownwardSpeed;

            for (int i = 0; i < numPoints; i++)
            {
                totalTime += timeBetweenPoints;
                float dx = horizontalVelocity * totalTime;
                float dy = (downwardSpeed * totalTime) + (0.5f * gravity * Mathf.Pow(totalTime, 2));
                Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

                // Draw the line between the previous point and the next point
                Gizmos.color = Color.green;
                Gizmos.DrawLine(previousPoint, nextPoint);
                previousPoint = nextPoint;
            }
        }

        if (activateTrampolinePath)
        {
            int numPointsUp = 350;
            int numPointsDown = 150;
            float timeBetweenPointsUp = 0.01f;
            float timeBetweenPointsDown = 0.05f;
            Vector3 startPosition = transform.position;
            Vector3 previousPoint = startPosition;
            float totalTime = 0f;
            gravity = gravityUpwards;
            trampolineJumpSpeed = _actionComponent.trampolineJumpSpeed;


            bool goingUp = true;
            for (int i = 0; i < numPointsUp && goingUp; i++)
            {
                totalTime += timeBetweenPointsUp;
                float dx = horizontalVelocity * totalTime;
                float dy = (trampolineJumpSpeed * totalTime) + (0.5f * gravity * Mathf.Pow(totalTime, 2));
                Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

                if (nextPoint.y < previousPoint.y) goingUp = false;

                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(previousPoint, nextPoint);

                previousPoint = nextPoint;
            }

            gravity = gravityDownwards;
            startPosition = previousPoint;
            totalTime = 0;

            for (int i = 0; i < numPointsDown; i++)
            {
                totalTime += timeBetweenPointsDown;
                float dx = horizontalVelocity * totalTime;
                float dy = 0.5f * gravity * Mathf.Pow(totalTime, 2);

                Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(previousPoint, nextPoint);

                previousPoint = nextPoint;
            }
        }
    }
}

