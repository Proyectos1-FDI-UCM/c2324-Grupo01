using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region references
    private AudioSource _myAudioSource;

    [SerializeField]
    private GameObject Player;
    private MovementComponent movementComponent;
    private ActionComponent actionComponent;
    private Rigidbody2D playerRigidbody;

    [SerializeField]
    private GameObject StartRecordCollider; 
    #endregion

    #region properties
    private bool canCallMethod = true; // Temporary
    float playerPosX; // Temporary
    float starColliderPosX; // Temporary
    #endregion

    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();

        #region temporary // Delete for building

            actionComponent = Player.GetComponent<ActionComponent>();
            movementComponent = Player.GetComponent<MovementComponent>();
            playerRigidbody = Player.GetComponent<Rigidbody2D>();

            playerPosX = Player.transform.position.x;
            starColliderPosX = StartRecordCollider.transform.position.x;

            if (playerPosX < starColliderPosX) 
            {
                canCallMethod = false;
            }

        #endregion

    }
    private void Update()
    {
        // Temporary
        if (canCallMethod)
        {
            Sync();
        }
    }

    #region methods
    public void PlayMusic()
    {
        _myAudioSource.Play();
    }
    private void Sync() // Temporary
    {        
        float playerSpeed = movementComponent.speed;
        float playerVel = playerRigidbody.velocity.x;

        float time = (playerPosX - starColliderPosX)/playerSpeed;

        if (playerVel > 0.01f && Time.time > 2)
        {
            _myAudioSource.time = time;
            PlayMusic();
            canCallMethod = false;
        }
    }
    #endregion
}
