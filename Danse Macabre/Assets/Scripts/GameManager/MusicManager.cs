using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float delay = 0.5f;
    [SerializeField]
    private float soundCtr = 1;
    private float elapsedTime = 0f;
    #endregion

    #region references
    private AudioSource _myAudioSource;
    public AudioClip coinSound;
    #endregion

    // DO NOT DELETE
    // [SerializeField]
    // private GameObject Player;
    // private MovementComponent movementComponent;
    // private ActionComponent actionComponent;
    // private Rigidbody2D playerRigidbody;
    // [SerializeField]
    // private GameObject StartRecordCollider; 
    // #endregion
    // #region properties
    // private bool canCallMethod = true; // Temporary
    // float playerPosX; // Temporary
    // float starColliderPosX; // Temporary
    //#endregion

    #region properties
    //Singleton controlador del sonido
    //(habia problemas cuando el objeto se elimina muy rapido y no se reproducia el sonido, asi que todo el audio se va a reproducir aqui)
    private static MusicManager instance;
    public static MusicManager Instance
    {
        get { return instance; }
    }
    private bool isPlaying = false;
    #endregion

    #region methods
    public void PlayMusic()
    {
        _myAudioSource.Play();
    }
    public void PlaySoundEffect(AudioClip clip)
    {
        _myAudioSource.PlayOneShot(clip,soundCtr);
    }
    // private void Sync() // Temporary
    // {        
    //     float playerSpeed = movementComponent.speed;
    //     float playerVel = playerRigidbody.velocity.x;

    //     float time = (playerPosX - starColliderPosX)/playerSpeed;

    //     if (playerVel > 0.01f && Time.time > 2)
    //     {
    //         _myAudioSource.time = time;
    //         PlayMusic();
    //         canCallMethod = false;
    //     }
    // }
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();

        // actionComponent = Player.GetComponent<ActionComponent>();
        // movementComponent = Player.GetComponent<MovementComponent>();
        // playerRigidbody = Player.GetComponent<Rigidbody2D>();

        // playerPosX = Player.transform.position.x;
        // starColliderPosX = StartRecordCollider.transform.position.x;

        // if (playerPosX < starColliderPosX) 
        // {
        //     canCallMethod = false;
        // }
    }
    void Update()
    {
        //Calculos para el momento inicial de reproducir el BGM
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= delay && !isPlaying)
        {
            isPlaying = true;
            PlayMusic();
        }
        
        // if (canCallMethod)
        // {
        //     Sync();
        // }
    }

}
