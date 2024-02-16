using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    float speed;

    bool canMove = true;
    float maxSpeed = 0.1f;
    float verticalSpeed = 0;

    Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
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
}
