using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _jumpSpeed = 1.0f;

    private float groundCheckDistance = 0.1f;
    #endregion

    #region references
    private Transform _myTransform;
    private Rigidbody2D _myRB;
    [SerializeField]
    private LayerMask groundLayer; // Capa que representa el suelo
    #endregion

    #region properties
    private float _verticalSpeed;
    #endregion 

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
    public void Stomp()
    {
        gameObject.SetActive(true);
    }
    public void Slide()
    {
        _myTransform.localScale = new Vector3(0.5f, 0.5f, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
