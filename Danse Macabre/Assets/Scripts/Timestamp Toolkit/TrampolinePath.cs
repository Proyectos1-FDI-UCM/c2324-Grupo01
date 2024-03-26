using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinePath : MonoBehaviour
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
    public int numPointsUp = 350;
    public int numPointsDown = 150;
    public float timeBetweenPointsUp = 0.01f;
    public float timeBetweenPointsDown = 0.05f;

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
        jumpSpeed = _actionComponent.trampolineJumpSpeed;
        gravityUpwards = Physics2D.gravity.y * _actionComponent.originalGravityScale; // for vel.y > 0
        gravityDownwards = gravityUpwards * _actionComponent.gravityFactor; // for vel.y < 0
        

        Vector3 startPosition = transform.position;
        Vector3 previousPoint = startPosition;
        float totalTime = 0f;
        gravity = gravityUpwards;


        bool goingUp = true;
        for (int i = 0; i < numPointsUp && goingUp; i++)
        {
            totalTime += timeBetweenPointsUp;
            float dx = horizontalVelocity * totalTime;
            float dy = (jumpSpeed * totalTime) + (0.5f * gravity * Mathf.Pow(totalTime, 2));
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

    void Start()
    {
    }

    void Update()
    {
    }
}
