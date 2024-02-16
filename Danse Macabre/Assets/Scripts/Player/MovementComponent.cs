using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float maxSpeed = -100f;
    float verticalSpeed = 0;

    public bool canMove = true;
    public bool grounded;
    private float lastYposition;

    Transform myTransform;
    Rigidbody2D myRigidBody;

    
    

    void Start()
    {
        myTransform = transform;
        myRigidBody = GetComponent<Rigidbody2D>();

        lastYposition = transform.position.y;
    }

    void Update()
    {
        //Debug.Log(lastYposition + " " + transform.position.y);

        CheckGrounded();
        Gravity();
        Move();
    }

    private void Move()
    {
        if (canMove)
        {
            Vector3 distanceToMove = new Vector3 (speed, verticalSpeed, 0) * Time.deltaTime;

            myRigidBody.MovePosition(myTransform.position + distanceToMove);
        }
    }
    private void Gravity()
    {
        // Debug.Log(verticalSpeed + Physics.gravity.y * 0.01f);
        verticalSpeed = Mathf.Max(maxSpeed, verticalSpeed + Physics.gravity.y * 0.01f);
        
    }

    private void CheckGrounded()
    {
        // checks if y has changed since last frame, and if the player is currently falling
        // if they are falling but their y hasn't changed, it means they touched the ground
        float positionDiff = (lastYposition - transform.position.y);
        
        grounded = ((positionDiff < 0.001f) && (verticalSpeed < -20));
        if (grounded)
        {
            verticalSpeed = 0;
        }
        Debug.Log(grounded + " " + positionDiff + " " + verticalSpeed);
        lastYposition = transform.position.y;
    }
    
}

