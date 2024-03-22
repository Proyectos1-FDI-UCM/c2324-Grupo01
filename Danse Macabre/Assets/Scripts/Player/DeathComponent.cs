using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathComponent : MonoBehaviour
{
    #region references
    [SerializeField]
    private LayerMask deathLayers;
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Check if the collided object's layer is in the deathLayers LayerMask (INSPECTOR)
        if ((deathLayers.value & (1 << collision.gameObject.layer)) != 0)
        {
            GameManager.Instance.PlayerHasDied();
        }
        else
        {
            // The collision occurred with an object in another layer
            Debug.Log("Collision with a non-death layer.");
        }
    


        // MovementComponent _player = collision.gameObject.GetComponent<MovementComponent>();
        // if (_player != null)
        // {
        //     _scoreManager.SaveFinalScore();
        //     //Guardar el nombre de la escena anterior para el bot�n restart
        //     PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        //     //Cambiar escena de muerte
        //     SceneManager.LoadScene(4);
        // }
    }

    void Start()
    {
        // _gameManager = FindObjectOfType<GameManager>();
        // _scoreManager = FindObjectOfType<ScoreManager>();
    }
}

