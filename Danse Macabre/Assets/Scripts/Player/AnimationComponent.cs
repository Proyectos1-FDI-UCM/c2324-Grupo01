using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    #region references
    private ActionComponent _actionComponent;
    private ActionComponent Action;
    private Animator _myAnimator;
    private Rigidbody2D _myRigidBody;
    private float verticalVelocity;
    [SerializeField] private TempoManager _Tempo;
    #endregion
    private float AnimSpeed;
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _actionComponent = GetComponent<ActionComponent>();
        _myRigidBody = GetComponent<Rigidbody2D>();
        //_Tempo.SecondsPerTick
        //Debug.Log(_Tempo.SecondsPerTick);
        AnimSpeed = _Tempo.SecondsPerTick * 3;
        _myAnimator.SetFloat("RunSpeed", AnimSpeed);
    }
    void Update()
    {
        if (_actionComponent.currentAction == ActionComponent.Action.Running) _myAnimator.SetInteger("State", 0);
        else if (_actionComponent.currentAction == ActionComponent.Action.Jumping) _myAnimator.SetInteger("State", 1);
        else if (_actionComponent.currentAction == ActionComponent.Action.Stomping) _myAnimator.SetInteger("State", 2);
        else if (_actionComponent.currentAction == ActionComponent.Action.Sliding) _myAnimator.SetInteger("State", 3);
        else if (_actionComponent.currentAction == ActionComponent.Action.Dashing 
                || _actionComponent.currentAction == ActionComponent.Action.Falling) _myAnimator.SetInteger("State", 5);


        // verticalVelocity = _myRigidBody.velocity.y;
        // if (_actionComponent.IsGrounded())
        // {
        //     if (_actionComponent.isSliding == true)
        //     {
        //         //Debug.Log("Sliding");
        //         _myAnimator.SetInteger("State", 3);
        //     }
        //     else
        //     {
        //         //Debug.Log("running");
        //         _myAnimator.SetInteger("State", 0);
        //     }

            
        // }
        // else 
        // {
        //     if (_actionComponent.isDashing == true)
        //     {
        //         //Debug.Log("Dashing");
        //         _myAnimator.SetInteger("State", 5); //esto seria dash en el futuro
        //     }
        //     else //if (_actionComponent.actionState == ActionComponent.ActionStateEnum)
        //     if (_actionComponent.isStomping == true)
        //     {
        //        // Debug.Log("stomping");
        //         _myAnimator.SetInteger("State", 2);
        //     }
        //     else
        //     {
        //        if (verticalVelocity > 0)
        //         {
        //            // Debug.Log("jumping");
        //             _myAnimator.SetInteger("State", 1);
        //         }
        //         else if (verticalVelocity < 0)
        //         {
        //            // Debug.Log("falling");
        //             _myAnimator.SetInteger("State", 5);
        //         }
        //         else
        //         {
        //            // Debug.Log("Air static");
        //             _myAnimator.SetInteger("State", 5);
        //         }
        //     }
            
            // /* 
            // 0 = run
            // 1 = Jump
            // 2 = Stomp
            // 3 = Slide
            // 4 = Air static
            // 5 = Falling
            // */
        //}
        
    }
    /// <summary>
    /// Deactivate animation when the player dies.
    /// </summary>
    public void ToggleAnimationOff()
    {
        _myAnimator.enabled = false;
    }
}
