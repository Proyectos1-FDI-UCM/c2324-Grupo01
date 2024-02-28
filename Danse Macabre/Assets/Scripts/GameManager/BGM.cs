using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float delay = 1.0f;
    private float _elapsedTime = 0f;
    #endregion
    #region references
    private AudioSource _myAudioSource;
    #endregion
    #region properties
    private bool _isPlaying = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (!_isPlaying && _elapsedTime>= delay)
        {
            _isPlaying = true;
            _myAudioSource.Play();
        }
    }
}
