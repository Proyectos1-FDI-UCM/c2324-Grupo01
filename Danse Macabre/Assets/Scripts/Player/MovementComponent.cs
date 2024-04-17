using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    #region parameters
    //private float speedVarianceAdmited = 0.05f;
    #endregion
    
    #region references
    [SerializeField]
    private Transform myTransform;
    private Rigidbody2D myRigidBody;
    [SerializeField]
    private TempoManager TempoManager;
    //private DeathComponent deathComponent;
    #endregion

    #region properties
    public float speed;
    public bool canMove = true;
    #endregion

    private void Awake()
    {
        myTransform = transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        //deathComponent = GetComponent<DeathComponent>();
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
    // void Update()
    // {
    //     if (deathComponent.CheckPlayerIsAlive() && math.abs(myRigidBody.velocity.x - speed) > speedVarianceAdmited)
    //     {
    //         Autoscroll();
    //     }
    // }



    #region methods
    public void Autoscroll()
    {
        speed = GameManager.Instance.PlayerSpeed;
        //myRigidBody.velocity = new(speed, myRigidBody.velocity.y);
        myRigidBody.velocity = Vector2.right * speed;
        GameManager.Instance.playerCanBeKilled = true;
    }
    public void InitialPosition(Vector3 position)
    {
        myTransform.position = position;
    }
    #endregion
}
