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

    bool canMove = true;
    
    float verticalSpeed = 0;

    Transform myTransform;
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log(verticalSpeed + Physics.gravity.y * 0.01f);
        verticalSpeed = Mathf.Max(maxSpeed, verticalSpeed + Physics.gravity.y * 0.01f);

        /*Other iterations:
        verticalSpeed = Mathf.Max(minSpeed, verticalSpeed + Physics.gravity.y * Time.deltaTime);
        verticalSpeed += Physics.gravity.y * 0.01f *Time.deltaTime;*/
    }
}
