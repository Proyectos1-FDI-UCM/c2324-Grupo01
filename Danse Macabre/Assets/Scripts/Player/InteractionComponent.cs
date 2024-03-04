using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    #region parameters
    // [SerializeField]
    // private float impulseTrampolin = 12;
    #endregion

    #region references
    private Rigidbody2D _myRB;
    private BoxCollider2D _myCollider;
    private ActionComponent _myActionComponent;
    [SerializeField]
    private LayerMask _platformLayer;
    #endregion

    #region properties
    #endregion

    #region methods
    // DONE
    // Rebote del trampol�n con collider isTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        // if (_myActionComponent.isStomping)
        // {
        //     if (obj.CompareTag("Trampolin") || obj.CompareTag("Enemigo"))
        //     {
        //         _myRB.velocity = new Vector2(_myRB.velocity.x, 0);
        //         _myRB.AddForce(impulseTrampolin * Vector2.up, ForceMode2D.Impulse);
        //         _myActionComponent.isStomping = false;
        //     }
        // }

        if (obj.CompareTag("Moneda dash"))
        {
            _myActionComponent.canDash = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (_myActionComponent.isStomping || _myActionComponent.isDashing || _myActionComponent.isSliding)
        {
           /* if (obj.CompareTag("Enemigo"))
            {
                // Animaci�n matando enemigo
               Destroy(obj); // destroy enemigo
            }
           */
            if (obj.CompareTag("Caja"))
            {
                /* Animaci�n destruyendo caja */
                Destroy(obj); // destroy caja
            }
        }
    }

    #endregion

    void Start()
    {
        _myRB = GetComponent<Rigidbody2D>();
        _myActionComponent = GetComponent<ActionComponent>();
        _myCollider = GetComponent<BoxCollider2D>();

        
    }

    void Update()
    {

    }
}
