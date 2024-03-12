using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerSFXController : MonoBehaviour
{
    #region references
    ActionComponent _myAction;
    [SerializeField]
    private AudioClip[] audioClips = new AudioClip[4]; 
    #endregion
    void Start()
    {
        _myAction = GetComponent<ActionComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( _myAction != null )
        {
            
            if (_myAction._isJumping) MusicManager.Instance.PlaySoundEffect(audioClips[0]); //jump
            else if (_myAction.isStomping) MusicManager.Instance.PlaySoundEffect(audioClips[1]); //stomp
            else if (_myAction.isSliding) MusicManager.Instance.PlaySoundEffect(audioClips[2]); //slide
            else if (_myAction.isDashing) MusicManager.Instance.PlaySoundEffect(audioClips[3]); //dash
        }
    }
}
