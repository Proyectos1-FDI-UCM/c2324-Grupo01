using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region references
    private AudioSource _myAudioSource;
    #endregion

    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
    }

    #region methods
    public void PlayMusic()
    {
        _myAudioSource.Play();
    }
    #endregion
}
