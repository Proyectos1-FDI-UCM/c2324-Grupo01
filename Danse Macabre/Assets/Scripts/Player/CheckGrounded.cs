using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour
{
    private float lastYposition;
    private bool grounded;

    void Start()
    {
        lastYposition = transform.position.y;
    }

    void Update()
    {
        grounded = (lastYposition == transform.position.y); // Checks if Y has changed since last frame
        if (grounded)
        {
            Debug.Log("grounded");
        }
        lastYposition = transform.position.y;
    }
}
