using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float delay = 0.5f;
    private float elapsedTime = 0f;
    #endregion

    #region references
    private AudioSource _myAudioSource;
    public AudioClip coinSound;
    #endregion

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
        _myAudioSource.PlayOneShot(clip);
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
    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
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
    }

}
