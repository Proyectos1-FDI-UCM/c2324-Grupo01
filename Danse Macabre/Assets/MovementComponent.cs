using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    float speed;

    Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.position = new Vector3(myTransform.position.x + (speed * Time.deltaTime), myTransform.position.y, myTransform.position.z);
    }
}
