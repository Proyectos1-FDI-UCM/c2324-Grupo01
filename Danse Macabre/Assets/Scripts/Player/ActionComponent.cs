using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _jumpSpeed = 9.0f;
    [SerializeField]
    private float groundCheckDistance = 0.55f; // Estaba pequeña
    [SerializeField]
    private float impulseStomp = 20;
    [SerializeField]
    private float dashDuration = 2.0f;
    private float dashEndTime = 0;
    #endregion

    #region references
    private Transform _myTransform;
    private Rigidbody2D _myRB;
    private BoxCollider2D _myCollider;
    [SerializeField]
    private LayerMask groundLayer; // Capa que representa el suelo
    #endregion

    #region properties
    private float _verticalSpeed;
    public bool isStomping = false; // sirve para habilitar rebote del trampolin
    // for dashing:
    public bool isDashing = false;
    public bool canDash = false;
    // for sliding:
    public bool isSliding = false;
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
            // a lo mejor _myCollider.transform.localScale?
            _myTransform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
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
    }

    void Update()
    {
        // TERMPORARIOOOOOOOO candash:
        if (IsGrounded()) canDash = true;
    }
}
