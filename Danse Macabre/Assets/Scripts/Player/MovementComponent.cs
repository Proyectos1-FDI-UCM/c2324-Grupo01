using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    #region references
    [SerializeField]
    private Transform myTransform;
    private Rigidbody2D myRigidBody;
    [SerializeField]
    private TempoManager TempoManager;
    #endregion

    #region properties
    public float speed;
    public bool canMove = true;
    #endregion

    private void Awake()
    {
        myTransform = transform;
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        // myTransform = transform;
        // myRigidBody = GetComponent<Rigidbody2D>();
        
        //speed = TempoManager.PlayerSpeed;
        //GameManager.Instance.PlayerSpeed = speed;
        //Debug.Log("Movement: Speed = " +  speed); 

        //speed = GameManager.Instance.PlayerSpeed;
        
        //Autoscroll();
    }

    #region methods
    public void Autoscroll()
    {
        speed = GameManager.Instance.PlayerSpeed;
        myRigidBody.velocity = Vector2.right * speed;
        GameManager.Instance.playerCanBeKilled = true;
    }
    public void InitialPosition(Vector3 position)
    {
        myTransform.position = position;
    }
    #endregion
}
