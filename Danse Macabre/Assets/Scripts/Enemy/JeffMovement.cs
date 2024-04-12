using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JeffMovement : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float Distance = 5.0f;
    [SerializeField]
    private float JeffSpeed = 2.0f;
    #endregion
    #region references
    private float newY;
    private Transform _mytransform;
    private Vector3 StartPosition;
    #endregion
    #region methods
    private void MoveUpandDown() 
    {
        newY = Mathf.PingPong(Time.time * JeffSpeed, Distance);
        _mytransform.position = new Vector3(StartPosition.x, StartPosition.y + newY, StartPosition.z);
    }
    #endregion
    void Start()
    {
        _mytransform = transform;
        StartPosition = transform.position;
    }
    void Update()
    {
        MoveUpandDown();
    }
}
