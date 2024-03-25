using System.Collections;
using System.Collections.Generic;
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
            if (_playerActionComponent.isStomping || _playerActionComponent.isDashing || _playerActionComponent.isSliding)
            {
                _enemyAnimation.DeathAnimation(); //Muere enemigo
                _scoreManager.AddPoints(_enemyValue, 1);
                //tipo de punto, 0=monedas, 1=enemigo, 2=objeto
                Invoke("DestroyEnemy", _destroyTime);
                if (BouncyEnemy)
                {
                    _playerActionComponent.Bounce();
                    _playerActionComponent.isStomping = false;
                }
            }
            else 
            {
                // _scoreManager.SaveFinalScore();
                // //Guardar el nombre de la escena anterior para el bot�n restart
                // PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
                // //Cambiar escena de Muerte
                // SceneManager.LoadScene(4);
            }
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
