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
    private AnimationComponent _animationComponent;
    private Rigidbody2D _RB;
    [SerializeField]
    private Canvas _DeathFilter;
    [SerializeField]
    private DeathFilterColor _DeathFilterColor;
    #endregion

    int layerValueEnemy;
    int layerValueTraps;

    private bool PlayerAlive;


    private void Start()
    {
        _movementComponent = GetComponent<MovementComponent>();
        _actionComponent = GetComponent<ActionComponent>();
        _RB = GetComponent<Rigidbody2D>();
        _animationComponent = GetComponent<AnimationComponent>();

        layerValueEnemy = LayerMask.NameToLayer("Enemies");
        layerValueTraps = LayerMask.NameToLayer("Traps");
        _DeathFilter.enabled = false;
        PlayerAlive = true;
    }

    private void Update()
    {
        CheckVelocityChange();
    }

    #region methods
    private void Death()
    {
        if (GameManager.Instance.PlayerCanBeKilled())
        {
            PlayerAlive = false;
            _RB.velocity = Vector3.zero;
            MusicManager.Instance.StopPlayingSong();
            _animationComponent.ToggleAnimationOff();
            _DeathFilter.enabled = true;
            _DeathFilterColor.ColorChange();
            Invoke("CallPlayerDeath", 1.2f);
        }
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

        if (collision.gameObject.layer == layerValueEnemy && (_actionComponent.isStomping || _actionComponent.isDashing))
        {
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<EnemyAnimation>().DeathAnimation();
            enemy.GetComponent<EnemyInteractionComponent>().DestroyEnemy();
            
            if (enemy.GetComponent<EnemyInteractionComponent>().BouncyEnemy)
            {
                _actionComponent.isStomping = false;
                _actionComponent.Bounce();
            }
        }
        else if (collision.gameObject.layer == layerValueEnemy && !(_actionComponent.isStomping || _actionComponent.isDashing) && PlayerAlive)
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
        if (collision.gameObject.layer == layerValueEnemy && (_actionComponent.isStomping || _actionComponent.isDashing))
        {
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<EnemyAnimation>().DeathAnimation();
            enemy.GetComponent<EnemyInteractionComponent>().DestroyEnemy();

            if (enemy.GetComponent<EnemyInteractionComponent>().BouncyEnemy)
            {
                _actionComponent.isStomping = false;
                _actionComponent.Bounce();
            }
        }
        else if (collision.gameObject.layer == layerValueEnemy && !(_actionComponent.isStomping || _actionComponent.isDashing) && PlayerAlive)
        {
            Death();
        }
    }
    #endregion
}

