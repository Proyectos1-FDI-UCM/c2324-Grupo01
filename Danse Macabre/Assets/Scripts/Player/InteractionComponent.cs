using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float impulseTrampolin = 12;
    #endregion

    #region references
    private Rigidbody2D _myRB;
    private ActionComponent _myActionComponent;
    #endregion

    #region properties
    #endregion

    #region methods
    // DONE
    // Rebote del trampolín con collider isTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (_myActionComponent.isStomping)
        {
            if (obj.CompareTag("Trampolin"))
            {
                _myRB.velocity = Vector2.zero;
                _myRB.AddForce(impulseTrampolin * Vector2.up, ForceMode2D.Impulse);
                _myActionComponent.isStomping = false;
            }
        }

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
            if (obj.CompareTag("Enemigo"))
            {
                /* Animación matando enemigo */
                Destroy(obj); // destroy enemigo
            }
            if (obj.CompareTag("Caja"))
            {
                /* Animación destruyendo caja */
                Destroy(obj); // destroy caja
            }
        }
    }
    #endregion

    void Start()
    {
        _myRB = GetComponent<Rigidbody2D>();
        _myActionComponent = GetComponent<ActionComponent>();
    }

    void Update()
    {

    }
}
