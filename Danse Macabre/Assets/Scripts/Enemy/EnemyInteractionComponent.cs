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

    [SerializeField]
    private int _enemyValue = 5;
    #endregion
    #region references
    private ActionComponent _playerActionComponent;
    private GameManager _gameManager;
    private EnemyAnimation _enemyAnimation;
    private ScoreManager _scoreManager;
    [SerializeField]
    private bool BouncyEnemy;
    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        MovementComponent _player = other.GetComponent<MovementComponent>();
        if (_player != null)
        {
            if (_playerActionComponent.isStomping || _playerActionComponent.isDashing)
            {
                _enemyAnimation.DeathAnimation(); //Muere enemigo
                _scoreManager.AddEnemyPoints(_enemyValue);
                Invoke("DestroyEnemy", _destroyTime);
                if (BouncyEnemy)
                {
                    _playerActionComponent.Bounce();
                }
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
        _scoreManager = FindObjectOfType<ScoreManager>();
        _enemyAnimation= GetComponent<EnemyAnimation>();
    }
}
