using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathComponent : MonoBehaviour
{
    #region references
    [SerializeField]
    private LayerMask deathLayers;
    private MovementComponent _movementComponent;
    private ActionComponent _action;
    private Rigidbody2D _RB;
    #endregion

    int layerValueEnemy = LayerMask.NameToLayer("Enemies");
    int layerValueTraps = LayerMask.NameToLayer("Traps");


    private void Start()
    {
        _movementComponent = GetComponent<MovementComponent>();
        _action = GetComponent<ActionComponent>();
        _RB = GetComponent<Rigidbody2D>();
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
        GameManager.Instance.PlayerHasDied();
    } 
    private void CheckVelocityChange()
    {
        if (_RB.velocity.x < _movementComponent.speed - 0.1f)
            Death();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (deathLayers.value == layerValueTraps)
        {
            Death();
        }
        if (deathLayers.value == layerValueEnemy && !(_action.isStomping || _action.isSliding || _action.isDashing))
        {
            Death();
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (deathLayers.value == layerValueTraps)
        {
            Death();
        }
        if (deathLayers.value == layerValueEnemy && !(_action.isStomping || _action.isSliding || _action.isDashing))
        {
            Death();
        }
    }
    #endregion
}

