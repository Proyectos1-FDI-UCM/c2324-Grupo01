using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ActionComponent : MonoBehaviour
{
    #region parameters
    //Saltos
    [SerializeField]
    private float _jumpSpeed;
    //La altura maxima que alcanza el salto
    [SerializeField]
    private float _maxHeight = 9.0f;

    [SerializeField]
    private float groundCheckDistance = 0.55f; // Estaba pequeña
    [SerializeField]
    private float impulseStomp = 20;
    [SerializeField]
    private float dashDuration = 2.0f;
    private float dashEndTime = 0;

    //rango de tiempo que puedes hacer dash (cuando coges la moneda especial)
    [SerializeField]
    private float _dashElapsedTime = 0.0f;
    #endregion

    #region references
    private Transform _myTransform;
    private Rigidbody2D _myRB;
    private BoxCollider2D _myCollider;
    [SerializeField]
    private LayerMask groundLayer; // Capa que representa el suelo

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
    #endregion

    #region methods
    private bool IsGrounded()
    {
        //Si hay algo en colision con el personaje esta en el suelo
        //Layer: Plataformas
        //Poner un ColisionTransform en los pies del personaje

        RaycastHit2D hit = Physics2D.Raycast(_myTransform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }
    public void Jump()
    {
        //JUMP 
        if (IsGrounded())
        {
            isStomping = false;
            _myRB.velocity = new Vector2(_myRB.velocity.x, _jumpSpeed);
        }
    }

    public void Stomp()
    {
        if (!IsGrounded() && !isStomping)
        {
            isDashing = false;
            isStomping = true;
            _myRB.AddForce(impulseStomp * Vector2.down, ForceMode2D.Impulse);
        }
    }

    public void Slide()
    {
        // DASH
        if (!IsGrounded() && canDash)
        {
            isDashing = true;
            canDash = false;
            StartCoroutine(Dash());
        }
        else if (IsGrounded())
        {
            myCollider.offset = new Vector2(SlideCollisionOffsetX, SlideCollisionOffsetY);
            myCollider.size = new Vector2(SlideCollisionSizeX, SlideCollisionSizeY);
        }
    }
    public void SlideStop()
    {
        myCollider.offset = new Vector2(DefaultCollisionOffsetX, DefaultCollisionOffsetY);
        myCollider.size = new Vector2(DefaultCollisionSizeX, DefaultCollisionSizeY);
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

        while (Time.time < dashEndTime && isDashing)
        {
            _myRB.velocity = new Vector2(_myRB.velocity.x, 0);
            _myRB.AddForce(- Vector2.up * Physics.gravity, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
    }
    #endregion

    void Start()
    {
        _myTransform = transform;
        _myRB = GetComponent<Rigidbody2D>();
        _myCollider = GetComponent<BoxCollider2D>();

        myCollider = this.gameObject.GetComponent<BoxCollider2D>();
        myCollider.offset = new Vector2(DefaultCollisionOffsetX, DefaultCollisionOffsetY);
        myCollider.size = new Vector2(DefaultCollisionSizeX, DefaultCollisionSizeY);
    }

    void Update()
    {
        // TERMPORARIOOOOOOOO candash:
        if (IsGrounded())
        {
            canDash = false;
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
}
