using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    float maxSpeed = -100f;
    float verticalSpeed = 0;
    #endregion

    #region references
    private Transform myTransform;
    private Rigidbody2D myRigidBody;
    #endregion

    #region properties
    [SerializeField]
    float speed;
    public bool canMove = true;
    public bool grounded;
    private float lastYposition;
    #endregion

    void Start()
    {
        myTransform = transform;
        myRigidBody = GetComponent<Rigidbody2D>();

        lastYposition = transform.position.y;
    }

    void Update()
    {
        //Debug.Log(lastYposition + " " + transform.position.y);

        //CheckGrounded();
        Gravity();
        Move();
    }

    #region methods
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
        //Debug.Log(grounded + " " + positionDiff + " " + verticalSpeed);
        lastYposition = transform.position.y;
    }
    #endregion
}
