using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathComponent : MonoBehaviour
{
    #region references
    private GameManager _gameManager;
    private ScoreManager _scoreManager;
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        MovementComponent _player = collision.gameObject.GetComponent<MovementComponent>();
        if (_player != null)
        {
            _scoreManager.SaveFinalScore();
            //Cambiar escena de muerte
            SceneManager.LoadScene(4);
        }
    }

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();
    }
}

