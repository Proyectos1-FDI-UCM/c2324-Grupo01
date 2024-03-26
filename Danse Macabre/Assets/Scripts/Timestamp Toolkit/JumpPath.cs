using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class JumpPath : MonoBehaviour
{
    // [SerializeField]
    // private GameObject Player;
    private Rigidbody2D _rb;
    private ActionComponent _actionComponent;
    private MovementComponent _movementComponent;

    public float jumpSpeed;
    public float horizontalVelocity;
    public float gravityUpwards;
    public float gravityDownwards;
    private float gravity;
    public int numPoints = 250;
    public int numPointsDown = 150;
    public float timeBetweenPoints = 0.1f;

    void OnDrawGizmos()
    {
        // _actionComponent = Player.GetComponent<ActionComponent>();
        // _movementComponent = Player.GetComponent<MovementComponent>();
        // _rb = Player.GetComponent<Rigidbody2D>();

        _actionComponent = GetComponent<ActionComponent>();
        _movementComponent = GetComponent<MovementComponent>();
        _rb = GetComponent<Rigidbody2D>();

        // horizontalVelocity = _movementComponent.speed; // speed de autoscroll
        horizontalVelocity = _movementComponent.speed;
        jumpSpeed = _actionComponent._jumpSpeed;
        gravityUpwards = Physics2D.gravity.y * _actionComponent.originalGravityScale; // for vel.y > 0
        gravityDownwards = gravityUpwards * _actionComponent.gravityFactor; // for vel.y < 0
        

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
            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
