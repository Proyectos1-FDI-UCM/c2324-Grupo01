using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    #region references
    private ActionComponent _actionComponent;
    private Animator _myAnimator;
    #endregion
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _actionComponent = GetComponent<ActionComponent>();
    }
    void Update()
    {
        if (!_actionComponent.IsGrounded())
        {
            //if (_actionComponent._isJumping == true) _myAnimator.SetInteger("State", 1);   provisional
            //if (_actionComponent.isDashing == true) _myAnimator.SetInteger("State", 1);
            //if (_actionComponent.isStomping == true) _myAnimator.SetInteger("State", 1);
        }
        else 
        {
            //if (_actionComponent.isSliding == true) _myAnimator.SetInteger("State", 1);
        }
    }
}
