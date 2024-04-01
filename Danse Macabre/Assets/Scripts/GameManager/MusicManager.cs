using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region parameters
    #endregion

    #region references
    private AudioSource _myAudioSource;
    #endregion

    #region properties
    public float time;
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
