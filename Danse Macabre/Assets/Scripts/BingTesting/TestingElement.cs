using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingElement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector2.right * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = Vector2.left * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
