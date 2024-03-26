using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompPath : MonoBehaviour
{
    // [SerializeField]
    // private GameObject Player;
    private Rigidbody2D _rb;
    private ActionComponent _actionComponent;
    private MovementComponent _movementComponent;

    public float downwardSpeed;
    public float horizontalVelocity;
    public float gravityDownwards;
    private float gravity;
    public int numPoints = 15;
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
        downwardSpeed = - _actionComponent.stompDownwardSpeed;
        gravityDownwards = Physics2D.gravity.y * _actionComponent.originalGravityScale * _actionComponent.gravityFactor;
        

        Vector3 startPosition = transform.position;
        Vector3 previousPoint = startPosition;
        float totalTime = 0f;
        gravity = gravityDownwards;


        for (int i = 0; i < numPoints; i++)
        {
            totalTime += timeBetweenPoints;
            float dx = horizontalVelocity * totalTime;
            float dy = (downwardSpeed * totalTime) + (0.5f * gravity * Mathf.Pow(totalTime, 2));
            Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

            // Draw the line between the previous point and the next point
            Gizmos.color = Color.cyan;
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
