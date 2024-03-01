using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    #region parameters
    #endregion

    #region references
    [SerializeField]
    private Transform myTransform;
    private Rigidbody2D myRigidBody;
    [SerializeField]
    private TempoManager TempoManager;

    // DO NOT DELETE
    // [SerializeField]
    // private GameObject MusicManager;
    // private AudioSource music;
    #endregion

    #region properties
    public float speed;
    public bool canMove = true;
    //private bool canCallMethod = true;
    #endregion

    void Start()
    {
        myTransform = transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        
        //music = MusicManager.GetComponent<AudioSource>();

        
        //speed = 1;
        speed = TempoManager.PlayerSpeed;
        //Debug.Log("Movement: Speed" +  speed); 
        
         
        Autoscroll();
        //lastYposition = transform.position.y;
    }
    
    void Update()
    {
        // DO NOT DELETE!
        // if (canCallMethod && Time.time > 2)
        // {
        //     Autoscroll();
        //     canCallMethod = false; 
        // }
    }

    #region methods
    private void Autoscroll()
    {
        myRigidBody.velocity = Vector2.right * speed;
    }
    #endregion
}
