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
    private float impulseTrampolin = 12;
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
    private bool isStomping = false; // sirve para habilitar rebote del trampolin
    // for dashing:
    private bool isDashing = false;
    private bool canDash = false;
    #endregion

    #region methods
    //CAMBIAD LO QUE HACE CADA METODO!! (LO QUE HAY ES PARA PROBAR!)
    private bool IsGrounded()
    {
        // Realiza un Raycast hacia abajo desde los pies del jugador
        RaycastHit2D hit = Physics2D.Raycast(_myTransform.position, Vector2.down, groundCheckDistance, groundLayer);

        // Si el Raycast golpea algo en la capa del suelo, el jugador está en el suelo
        return hit.collider != null;

    }
    public void Jump()
    {
        if (IsGrounded())
        {
            _myRB.velocity = new Vector2(_myRB.velocity.x, _jumpSpeed);
        }
    }

    // DONE
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

    // DONE
    // Rebote del trampolín con collider isTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (isStomping && obj.CompareTag("Trampolin"))
        {
            _myRB.velocity = Vector2.zero;
            _myRB.AddForce(impulseTrampolin * Vector2.up, ForceMode2D.Impulse);
            isStomping = false;
        }
        if (obj.CompareTag("Moneda dash"))
        {
            canDash = true;
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
            _myRB.velocity = Vector2.zero;
            _myRB.AddForce(- Vector2.up * Physics.gravity, ForceMode2D.Force);
            yield return null;
        }

        isDashing = false;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myRB = GetComponent<Rigidbody2D>();
        _myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // TERMPORARIOOOOOOOO candash:
        if (IsGrounded()) canDash = true;
    }
}
