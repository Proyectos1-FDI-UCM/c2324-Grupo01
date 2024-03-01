using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathComponent : MonoBehaviour
{
    #region references
    private GameManager _gameManager;
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        MovementComponent _player = collision.gameObject.GetComponent<MovementComponent>();
        if (_player != null)
        {
            _gameManager.Muerte(); //muere personaje
        }
    }

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
}

