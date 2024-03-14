using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro.EditorUtilities;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ActionComponent : MonoBehaviour
{
    #region parameters
    //Saltos
    [SerializeField]
    private float _jumpSpeed;
    private float originalGravityScale;
    [SerializeField]
    private float gravityFactor = 0.90f;
    [SerializeField]
    private float groundCheckDistance = 0.55f;
    [SerializeField]
    private float impulseStomp = 20;
    [SerializeField]
    private float impulseTrampolin = 12;
    [SerializeField]
    private float dashDuration = 2.0f;
    private float dashEndTime = 0;

    //rango de tiempo que puedes hacer dash (cuando coges la moneda especial)
    [SerializeField]
    private float _dashElapsedTime = 0.0f;

    private float _originalPitch = 1f;
    [SerializeField]
    private float slideCTR = 0.15f;
    [SerializeField]
    private float dashCTR = 0.2f;
    [SerializeField]
    private float jumpCTR = 0.2f;
    [SerializeField]
    private float stompCTR = 0.1f;
    #endregion

    #region references
    private Transform _myTransform;
    private Rigidbody2D _myRB;
    private BoxCollider2D _myCollider;
    [SerializeField]
    private LayerMask groundLayer; // Capa que representa el suelo / Añadir trampoline!
    [SerializeField]
    private LayerMask trampolineLayer;


    // sfx
    private AudioSource _myAudioSource;
    [SerializeField] 
    private AudioClip _jumpSound;
    [SerializeField]
    private AudioClip _stompSound;
    [SerializeField]
    private AudioClip _slideSound;
    [SerializeField]
    private AudioClip _dashSound;
    
    //collider references
    [SerializeField]
    private float DefaultCollisionSizeX;
    [SerializeField]
    private float DefaultCollisionSizeY;
    [SerializeField]
    private float DefaultCollisionOffsetX;
    [SerializeField]
    private float DefaultCollisionOffsetY;
    [SerializeField]
    private float SlideCollisionSizeX;
    [SerializeField]
    private float SlideCollisionSizeY;
    [SerializeField]
    private float SlideCollisionOffsetX;
    [SerializeField]
    private float SlideCollisionOffsetY;

    BoxCollider2D myCollider;
    #endregion

    #region properties
    public bool isStomping = false; // sirve para habilitar rebote del trampolin
    // for dashing:
    public bool isDashing = false;
    public bool canDash = false;
    // for sliding:
    public bool isSliding = false;
    //for jumping
    public bool _isJumping = false;
    /*public enum ActionStateEnum { Dashing, Stomping, Jumping, sliding, None};
    public ActionStateEnum actionState = ActionStateEnum.None;*/
    private bool gravityChanged = false; // Bool to track if gravity scale is already updated to falling state
    #endregion

    #region methods
    public bool IsGrounded()
    {
        //Si hay algo en colision con el personaje esta en el suelo
        //Layer: Plataformas
        //Poner un ColisionTransform en los pies del personaje

        RaycastHit2D hit = Physics2D.Raycast(_myTransform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }
    private bool IsTrampolin()
    {
        RaycastHit2D hit = Physics2D.Raycast(_myTransform.position, Vector2.down, groundCheckDistance, trampolineLayer);
        return hit.collider != null;
    }

    public void Jump()
    {
        //JUMP 
        if (IsGrounded())
        {
            isStomping = false;
            _isJumping = true;
            _myAudioSource.PlayOneShot(_jumpSound, jumpCTR);
            Bounce();
        }
    }
    public void Bounce()
    {
        // Este m�todo est?separado para que se pueda llamar a la hora de rebotar encima de un enemigo determinado sin comprobar grounded
        _myRB.velocity = new Vector2(_myRB.velocity.x, _jumpSpeed);
    }

    public void Stomp()
    {
        if (!IsGrounded() && !isStomping)
        {
            isDashing = false;
            isStomping = true;
            _myRB.AddForce(impulseStomp * Vector2.down, ForceMode2D.Impulse);
            
            if (!isDashing) _myAudioSource.PlayOneShot(_stompSound, stompCTR);
            
        }
    }

    public void Slide()
    {
        if (!IsGrounded() && canDash) //DASH
        {
            isDashing = true;
            canDash = false;
            if (!_myAudioSource.isPlaying)
            {
                _myAudioSource.pitch = 1;
                _myAudioSource.PlayOneShot(_dashSound, dashCTR);
            }
            StartCoroutine(Dash());
        }
        else if (IsGrounded())
        {
            isSliding = true;
            myCollider.offset = new Vector2(SlideCollisionOffsetX, SlideCollisionOffsetY);
            myCollider.size = new Vector2(SlideCollisionSizeX, SlideCollisionSizeY);
            if (!_myAudioSource.isPlaying)
            {
                _myAudioSource.pitch = 1;
                _myAudioSource.PlayOneShot(_slideSound,slideCTR);
                _myAudioSource.loop = true;
            }
        }
    }
    public void SlideStop()
    {
        myCollider.offset = new Vector2(DefaultCollisionOffsetX, DefaultCollisionOffsetY);
        myCollider.size = new Vector2(DefaultCollisionSizeX, DefaultCollisionSizeY);
        isSliding = false;
        _myAudioSource.Stop();
        _myAudioSource.loop = false;
        _myAudioSource.pitch = _originalPitch;
    }
    public void DashCountDown(float _time)
    {
        _dashElapsedTime = _time;
    }
    #endregion

    #region interface
    private IEnumerator Dash()
    {
        float startTime = Time.time;

        dashEndTime = startTime + dashDuration;
        _myRB.velocity = new Vector2(_myRB.velocity.x, 0);
        _myRB.gravityScale = 0;

        while (Time.time < dashEndTime && isDashing)
        {
            yield return new WaitForFixedUpdate();
        }

        _myRB.gravityScale = originalGravityScale;
        isDashing = false;
        _myAudioSource.Stop();
        _myAudioSource.pitch = _originalPitch;
    }

    #endregion

    void Start()
    {
        _myTransform = transform;
        _myRB = GetComponent<Rigidbody2D>();
        _myCollider = GetComponent<BoxCollider2D>();
        _myAudioSource = GetComponent<AudioSource>();
        _originalPitch = _myAudioSource.pitch;
        
        myCollider = this.gameObject.GetComponent<BoxCollider2D>();
        myCollider.offset = new Vector2(DefaultCollisionOffsetX, DefaultCollisionOffsetY);
        myCollider.size = new Vector2(DefaultCollisionSizeX, DefaultCollisionSizeY);

        originalGravityScale = _myRB.gravityScale;
    }

    void Update()
    {
        //if (IsGrounded() && (actionState == ActionComponent.ActionStateEnum.Stomping)) + state = none
        if (isStomping && IsTrampolin()) {
            print("sdfsdfsfsdfs");
            _myRB.AddForce(impulseTrampolin * Vector2.up, ForceMode2D.Impulse);
            isStomping = false;
        }
        if (IsGrounded())
        {
            //canDash = false;
            isStomping = false;
            _isJumping = false;
        }

        //Despues de coger la moneda, dash elapsed time se actualiza y entrar?al condicional
        if (_dashElapsedTime >= 0)
        {
            _dashElapsedTime -= Time.deltaTime;
            canDash = true;
        }
        else
        {
            canDash = false;
        }
        //Debug.Log("Dash time: "+_dashElapsedTime);

    }

    private void FixedUpdate()
    {
        if (_myRB.velocity.y < -0.01f && !gravityChanged)
        {
            _myRB.gravityScale *= gravityFactor;
            gravityChanged = true; // So it's called only once
        }
        else if (IsGrounded()) {
            _myRB.gravityScale = originalGravityScale;
            gravityChanged = false; // Reset when grounded
        }
    }
}
