using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathComponent : MonoBehaviour
{
    #region references
    // [SerializeField]
    // private LayerMask deathLayers;
    private MovementComponent _movementComponent;
    private ActionComponent _actionComponent;
    private Rigidbody2D _RB;
    [SerializeField]
    private Canvas _DeathFilter;
    [SerializeField]
    private DeathFilterColor _DeathFilterColor;
    #endregion

    int layerValueEnemy;
    int layerValueTraps;
    int layerValueTrampoline;

    private bool PlayerAlive;


    private void Start()
    {
        _movementComponent = GetComponent<MovementComponent>();
        _actionComponent = GetComponent<ActionComponent>();
        _RB = GetComponent<Rigidbody2D>();

        layerValueEnemy = LayerMask.NameToLayer("Enemies");
        layerValueTraps = LayerMask.NameToLayer("Traps");
        layerValueTrampoline = LayerMask.NameToLayer("Trampoline");
        _DeathFilter.enabled = false;
        PlayerAlive = true;
    }

    private void Update()
    {
        CheckVelocityChange();
    }

    #region methods
    /// <summary>
    /// Death conditions: change in velocity.x OR collision with Enemies or Traps layers.
    /// </summary>
    private void Death()
    {
        PlayerAlive = false;
        Debug.Log("Death()");
        _DeathFilter.enabled = true;
        _DeathFilterColor.ColorChange();
        Invoke("CallPlayerDeath", 1.2f);
        //CallPlayerDeath();
    }

    private void CallPlayerDeath()
    {
        GameManager.Instance.PlayerHasDied();
    }

    private void CheckVelocityChange()
    {
        if (_RB.velocity.x < _movementComponent.speed - 0.1f && PlayerAlive)
        {
            Death();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == layerValueTraps && PlayerAlive)
        {
            Death();
        }

        if (collision.gameObject.layer == layerValueEnemy && (_actionComponent.isStomping || _actionComponent.isSliding || _actionComponent.isDashing))
        {
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<EnemyAnimation>().DeathAnimation();
            //enemy.GetComponent<EnemyInteractionComponent>().DestroingEnemy();
            
            if (enemy.GetComponent<EnemyInteractionComponent>().BouncyEnemy)
            {
                _actionComponent.Bounce();
            }
            _actionComponent.isStomping = false;
        }
        else if (collision.gameObject.layer == layerValueEnemy && !(_actionComponent.isStomping || _actionComponent.isSliding || _actionComponent.isDashing) && PlayerAlive)
        {
            Death();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerValueTraps && PlayerAlive)
        {
            Death();
        }
        if (collision.gameObject.layer == layerValueEnemy && (_actionComponent.isStomping || _actionComponent.isSliding || _actionComponent.isDashing))
        {
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<EnemyAnimation>().DeathAnimation();
            enemy.GetComponent<EnemyInteractionComponent>().DestroyEnemy();

            if (enemy.GetComponent<EnemyInteractionComponent>().BouncyEnemy)
            {
                _actionComponent.Bounce();
            }
            _actionComponent.isStomping = false;
        }
        else if (collision.gameObject.layer == layerValueEnemy && !(_actionComponent.isStomping || _actionComponent.isSliding || _actionComponent.isDashing) && PlayerAlive)
        {
            Death();
        }
    }
    #endregion
}

