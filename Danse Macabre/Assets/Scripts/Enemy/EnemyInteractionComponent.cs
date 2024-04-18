using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyInteractionComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _destroyTime = 0.5f;

    [SerializeField]
    private int _enemyValue = 5;
    #endregion
    #region references
    private GameManager _gameManager;
    private ScoreManager _scoreManager;
    [SerializeField]
    public bool BouncyEnemy;

    [SerializeField] Collider2D Collider;
    #endregion
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     MovementComponent _player = other.GetComponent<MovementComponent>();
    //     if (_player != null)
    //     {
    //         if (_playerActionComponent.isStomping || _playerActionComponent.isDashing || _playerActionComponent.isSliding)
    //         {
    //             _enemyAnimation.DeathAnimation(); //Muere enemigo
    //             _scoreManager.AddPoints(_enemyValue, 1);
    //             //tipo de punto, 0=monedas, 1=enemigo, 2=objeto
            //     Invoke("DestroyEnemy", _destroyTime);
            //     if (BouncyEnemy)
            //     {
            //         _playerActionComponent.Bounce();
            //     }
            // }
            // else 
            // {
            //     // _scoreManager.SaveFinalScore();
            //     // //Guardar el nombre de la escena anterior para el bot�n restart
            //     // PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            //     // //Cambiar escena de Muerte
            //     // SceneManager.LoadScene(4);
            // }
        //}
    //}

    public void DestroyEnemy()
    {
        //tipo de punto, 0=monedas, 1=enemigo, 2=objeto
        _scoreManager.AddPoints(_enemyValue, 1);
        //Collider.enabled = false;
        Invoke("DestroyGameObject", _destroyTime);

    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public bool IsBouncy()
    {
        return BouncyEnemy;
    }
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();

        //tipo de punto, 0=monedas, 1=enemigo, 2=objeto
        MaxScoreCalculator.Instance.ObjectRegister(1, _enemyValue);
    }
}
