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
    private Rigidbody2D _RB;
    #endregion

    private void Start()
    {
        _movementComponent = GetComponent<MovementComponent>();
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
        // Check if the collided object's layer is in the deathLayers LayerMask (INSPECTOR)
        if ((deathLayers.value & (1 << collision.gameObject.layer)) != 0)
            Death();

    }
    #endregion
}

