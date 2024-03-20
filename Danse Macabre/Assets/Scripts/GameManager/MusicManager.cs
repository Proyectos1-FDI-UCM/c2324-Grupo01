using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region parameters
    // [SerializeField]
    // private float delay = 0.5f;
    // private float elapsedTime = 0f;
    #endregion

    #region references
    private AudioSource _myAudioSource;
    #endregion

    // For potential checkpoint
    // [SerializeField]
    // private GameObject Player;
    // private MovementComponent movementComponent;
    // private ActionComponent actionComponent;
    // private Rigidbody2D playerRigidbody;
    // [SerializeField]
    // private GameObject StartRecordCollider;

    #region properties
    // For potential checkpoint
    // private bool canCallMethod = true;
    // float playerPosX;
    // float starColliderPosX;
    #endregion

    #region properties
    private float pauseTime = 0f;
    //Singleton controlador del sonido
    //(habia problemas cuando el objeto se elimina muy rapido y no se reproducia el sonido, asi que todo el audio se va a reproducir aqui)
    private static MusicManager instance;
    public static MusicManager Instance
    {
        get { return instance; }
    }
    //private bool isPlaying = false;
    #endregion

    #region methods
    public void PlayMusic()
    {
        _myAudioSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip, float volume)
    {
        _myAudioSource.PlayOneShot(clip,volume);
        _myAudioSource.loop = false;
        
    }
    public void StopPlayingSong()
    {
        
        pauseTime = _myAudioSource.time;
        print(pauseTime);
        _myAudioSource.Stop();
    }
    public void PlayLoop(AudioClip clip, float volume)
    {
        _myAudioSource.PlayOneShot(clip,volume);
        _myAudioSource.loop = true;
    }

    public void ResumePlayingSong()
    {
        _myAudioSource.time = pauseTime;
        _myAudioSource.Play();
    }

    //private void Sync() // For potential checkpoint
    //{
        // float playerSpeed = movementComponent.speed;
        // float playerVel = playerRigidbody.velocity.x;

        // float time = (playerPosX - starColliderPosX)/playerSpeed;

        // if (playerVel > 0.01f && Time.time > 1)
        // {
        //     _myAudioSource.time = time;
        //     PlayMusic();
        //     canCallMethod = false;
        // }
    //}
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

        // For potential checkpoint
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

    //void Update()
    //{
        //Calculos para el momento inicial de reproducir el BGM
        // elapsedTime += Time.deltaTime;
        // if (elapsedTime >= delay && !isPlaying)
        // {
        //     isPlaying = true;
        //     print("player pos:" + Player.transform.position.x);
        //     PlayMusic();
        // }

        // For potential checkpoint
        // if (canCallMethod)
        // {
        //     Sync();
        // }
    //}

}
