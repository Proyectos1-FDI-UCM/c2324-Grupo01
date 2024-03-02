using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    #region references
    private ActionComponent _actionComponent;
    private Animator _myAnimator;
    private Rigidbody2D _myRigidBody;
    private float verticalVelocity;
    #endregion
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _actionComponent = GetComponent<ActionComponent>();
        _myRigidBody = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {
        verticalVelocity = _myRigidBody.velocity.y;
        if (_actionComponent.IsGrounded())
        {
            if (_actionComponent.isSliding == true)
            {
                Debug.Log("Sliding");
                _myAnimator.SetInteger("State", 3);
            }
            else
            {
                Debug.Log("running");
                _myAnimator.SetInteger("State", 0);
            }

            
        }
        else 
        {
            /*if (_actionComponent.isDashing == true)
            {
                Debug.Log("Dashing");
                _myAnimator.SetInteger("State", 3); //esto seria dash en el futuro
            }
            else *///if (_actionComponent.actionState == ActionComponent.ActionStateEnum)
            if (_actionComponent.isStomping == true)
            {
                Debug.Log("stomping");
                _myAnimator.SetInteger("State", 2);
            }
            else
            {
                if (verticalVelocity > 0)
                {
                    Debug.Log("jumping");
                    _myAnimator.SetInteger("State", 1);
                }
                else if (verticalVelocity < 0)
                {
                    Debug.Log("falling");
                    _myAnimator.SetInteger("State", 5);
                }
                else
                {
                    Debug.Log("Air static");
                    _myAnimator.SetInteger("State", 4);
                }
            }
            
            /* 
            0 = run
            1 = Jump
            2 = Stomp
            3 = Slide
            4 = Air static
            5 = Falling
            */
        }
        
    }
}
