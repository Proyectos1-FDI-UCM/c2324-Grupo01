using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    #region pearameters
    [SerializeField]
    private float speed;
    private float maxSpeed = 0.1f;
    private float verticalSpeed = 0;
    #endregion

    #region references
    private Transform myTransform;
    #endregion

    #region properties
    private bool canMove = true;
    #endregion

    #region methods
    private void Move()
    {
        if (canMove)
        {
            Debug.Log("MOVING " + speed + " " + verticalSpeed);
            myTransform.position = new Vector3(myTransform.position.x + (speed * Time.deltaTime), 
                myTransform.position.y + (verticalSpeed * Time.deltaTime), 
                myTransform.position.z);
        }
    }
    private void Gravity()
    {
        verticalSpeed = Mathf.Min(maxSpeed, verticalSpeed + Physics.gravity.y * 0.01f);

        /*Other iterations:
        verticalSpeed = Mathf.Max(minSpeed, verticalSpeed + Physics.gravity.y * Time.deltaTime);
        verticalSpeed += Physics.gravity.y * 0.01f *Time.deltaTime;*/
    }
    #endregion

    void Start()
    {
        myTransform = transform;
    }

    void Update()
    {
        Gravity();
        Move();
    }
}

