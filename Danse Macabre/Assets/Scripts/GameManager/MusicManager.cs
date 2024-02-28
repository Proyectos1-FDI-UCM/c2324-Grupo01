using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float delay = 0.5f;
    #endregion
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
        _myAudioSource.PlayDelayed(delay);
    }
    #endregion
}
