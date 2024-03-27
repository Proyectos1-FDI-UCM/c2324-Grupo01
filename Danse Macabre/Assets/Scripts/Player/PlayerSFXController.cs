using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerSFXController : MonoBehaviour
{
    #region references
    private ActionComponent _myAction;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip[] audioClips = new AudioClip[4]; 
    #endregion
    void Start()
    {
        _myAction = GetComponent<ActionComponent>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( _myAction != null )
        {
            
            if (_myAction._isJumping)
            {
                Debug.Log("Entrado");
                _audioSource.PlayOneShot(audioClips[0]);
            }
            else if (_myAction.isStomping)
            {
                _audioSource.PlayOneShot(audioClips[1]);
                Debug.Log("Entrando 2");
            }
        }
    }
}
