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
    public float time;
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
    public void ChangeTime(float time)
    {
        _myAudioSource.time = time;
    }

    public void LoadAllReferences()
    {
        _myAudioSource = GetComponent<AudioSource>();
    }
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

    private void Start()
    {
        LoadAllReferences();

    }

}
