using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPathDrawer : MonoBehaviour
{
    private Transform _dummyTransform;
    private Rigidbody2D _rb;
    private BoxCollider2D _dummyCollider;
    [SerializeField]
    private GameObject _player;
    private ActionComponent _actionComponent;
    [SerializeField]
    private LevelDataLoader _levelDataLoader;

    public float jumpSpeed;
    public float trampolineJumpSpeed;
    public float dashDuration;
    public float horizontalVelocity;
    private float gravityGoingUp;
    private float gravityGoingDown;
    private float gravity;
    private float groundCheckDistance = 0.55f;
    
    private float trampolinCheckDistance = 0.60f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask trampolineLayer;
    [SerializeField]
    private LayerMask enemyLayer;
    private int numPoints = 250;
    private int numPointsDown = 150;
    private float timeBetweenPoints = 0.02f;
    public bool JumpPath = true;
    public bool StompPath = true;
    public bool TrampolinePath = true;
    public bool DashPath = true;
    public bool detectGround = true;
    public bool detectTrampoline = false;
    public bool detectEnemyBellow = true;
    // public bool setCustomHorizontalVelocity = false;
    // public bool setCustomJumpSpeed = false;
    // public bool setCustomDashDuration = false;

    void OnDrawGizmos()
    {
        _actionComponent = _player.GetComponent<ActionComponent>();
        _rb = GetComponent<Rigidbody2D>();
        _dummyCollider = GetComponent<BoxCollider2D>();
        _dummyTransform = transform;

        // if (!setCustomHorizontalVelocity) horizontalVelocity = _levelDataLoader.GetCurrentScenePlayerSpeed();
        // if (!setCustomJumpSpeed) jumpSpeed = _actionComponent.jumpSpeed;
        // if(!setCustomDashDuration) dashDuration = _actionComponent.dashDuration;

        gravityGoingUp = Physics2D.gravity.y * _actionComponent.originalGravityScale; // for vel.y > 0
        gravityGoingDown = gravityGoingUp * _actionComponent.gravityFactor; // for vel.y < 0

        // JUMP PATH DRAWER
        if (JumpPath)
        {
            Vector3 startPosition = transform.position;
            Vector3 previousPoint = startPosition;
            float totalTime = 0f;

            gravity = gravityGoingUp;
            bool goingUp = true;

            for (int i = 0; i < numPoints && goingUp; i++)
            {
                totalTime += timeBetweenPoints;

                float dx = horizontalVelocity * totalTime;
                float dy = (jumpSpeed * totalTime) + (0.5f * gravity * Mathf.Pow(totalTime, 2));
                Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

                if (nextPoint.y < previousPoint.y){
                    nextPoint = previousPoint;
                    goingUp = false;
                }

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(previousPoint, nextPoint);

                previousPoint = nextPoint;
            }

            gravity = gravityGoingDown;
            startPosition = previousPoint;
            totalTime = 0;

            for (int i = 0; i < numPointsDown; i++)
            {
                totalTime += timeBetweenPoints;

                float dx = horizontalVelocity * totalTime;
                float dy = 0.5f * gravity * Mathf.Pow(totalTime, 2);
                Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(previousPoint, nextPoint);

                previousPoint = nextPoint;
            }
        }

        // STOMP PATH DRAWER
        if (StompPath)
        {
            Vector3 startPosition = transform.position;
            Vector3 previousPoint = startPosition;

            float totalTime = 0f;
            gravity = gravityGoingDown;
            float downwardSpeed = - _actionComponent.stompDownwardSpeed;

            for (int i = 0; i < numPoints; i++)
            {
                totalTime += timeBetweenPoints;

                float dx = horizontalVelocity * totalTime;
                float dy = (downwardSpeed * totalTime) + (0.5f * gravity * Mathf.Pow(totalTime, 2));
                Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(previousPoint, nextPoint);

                previousPoint = nextPoint;
            }
        }

        // TRAMPOLINE PATH DRAWER
        if (TrampolinePath)
        {
            int numPointsUp = 350;
            int numPointsDown = 150;
            float timeBetweenPointsUp = 0.01f;
            float timeBetweenPointsDown = 0.01f;

            Vector3 startPosition = transform.position;
            Vector3 previousPoint = startPosition;

            float totalTime = 0f;
            gravity = gravityGoingUp;
            trampolineJumpSpeed = _actionComponent.trampolineJumpSpeed;

            bool goingUp = true;

            for (int i = 0; i < numPointsUp && goingUp; i++)
            {
                totalTime += timeBetweenPointsUp;
                float dx = horizontalVelocity * totalTime;
                float dy = (trampolineJumpSpeed * totalTime) + (0.5f * gravity * Mathf.Pow(totalTime, 2));
                Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

                if (nextPoint.y < previousPoint.y){
                    nextPoint = previousPoint;
                    goingUp = false;
                }

                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(previousPoint, nextPoint);

                previousPoint = nextPoint;
            }

            gravity = gravityGoingDown;
            startPosition = previousPoint;
            totalTime = 0;

            for (int i = 0; i < numPointsDown; i++)
            {
                totalTime += timeBetweenPointsDown;

                float dx = horizontalVelocity * totalTime;
                float dy = 0.5f * gravity * Mathf.Pow(totalTime, 2);
                Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(previousPoint, nextPoint);

                previousPoint = nextPoint;
            }
        }

        // DASH PATH DRAWER
        if (DashPath)
        {
            Vector3 startPosition = transform.position;

            float dx = horizontalVelocity * dashDuration;
            Vector3 endPosition = startPosition + new Vector3(dx, 0, 0);

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(startPosition, endPosition);
        }

        // IS GROUNDED DETECTOR
        if (detectGround)
        {
            RaycastHit2D hit = Physics2D.Raycast(_dummyTransform.position, Vector2.down, groundCheckDistance, groundLayer);
            if (hit.collider != null) Gizmos.DrawSphere(_dummyTransform.position, 0.3f);
        }

        // DETECT TRAMPOLINE
        if (detectTrampoline)
        {
            RaycastHit2D hit = Physics2D.Raycast(_dummyTransform.position, Vector2.down, trampolinCheckDistance, trampolineLayer);
            Gizmos.color = Color.green;
            if (hit.collider != null) Gizmos.DrawSphere(_dummyTransform.position, 0.3f);
        }

        if (detectEnemyBellow)
        {
            RaycastHit2D hit = Physics2D.Raycast(_dummyTransform.position, Vector2.down, _dummyCollider.size.y/2, enemyLayer);
            Gizmos.color = Color.blue;
            if (hit.collider != null) Gizmos.DrawSphere(_dummyTransform.position, 0.3f);



        }
        
    }
}

