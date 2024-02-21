using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackComponent : MonoBehaviour
{
    #region references
    private ActionComponent _playerActionComponent;
    private GameManager _gameManager;
    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.GetComponent<MovementComponent>())
        {
            if (_playerActionComponent.isDashing || _playerActionComponent.isStomping)
            {
                Destroy(gameObject);
            }
            else _gameManager.Muerte();
        }
    }
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _playerActionComponent=GetComponent<ActionComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
