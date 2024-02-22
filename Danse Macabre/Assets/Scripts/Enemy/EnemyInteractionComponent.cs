using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyInteractionComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _destroyTime = 5f;
    #endregion
    #region references
    private ActionComponent _playerActionComponent;
    private GameManager _gameManager;
    private EnemyAnimation _enemyAnimation;
    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        MovementComponent _player = other.GetComponent<MovementComponent>();
        if (_player != null)
        {
            if (_playerActionComponent.isStomping || _playerActionComponent.isDashing)
            {
                _enemyAnimation.DeathAnimation(); //Muere enemigo
                Invoke("DestroyEnemy", _destroyTime);
            }
            else _gameManager.Muerte(); //muere personaje
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerActionComponent=FindObjectOfType<ActionComponent>();
        _enemyAnimation= GetComponent<EnemyAnimation>();
    }
}
