using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (TempoManager != null) speed = TempoManager.PlayerSpeed;
        //speed = 1;
        //speed = TempoManager.PlayerSpeed;
        //Debug.Log("Movement: Speed" +  speed); 
        
         
        Autoscroll(); // VOLVER
        //lastYposition = transform.position.y;
    }
    
    void Update()
    {

        if (myRigidBody.velocity.x < speed - 0.01f)
        {
            //Guardar el nombre de la escena anterior para el bot�n restart
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            GameManager.Instance.Muerte();
        }

        // NO QUITAR: SIRVE PARA LEVEL BUILDING
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
