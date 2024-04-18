using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScoreWin : MonoBehaviour
{
    #region references
    private ScoreManager _scoreManager;
    #endregion
    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MovementComponent _player = collision.GetComponent<MovementComponent>();
        if (_player != null)
        {
            _scoreManager.SaveFinalScore();


            //Cambiar escena de Victoria
            GameManager.Instance.ResetCheckpoint();
            SceneManager.LoadScene("Victoria");
        }
    }
    #endregion
    void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
    }
}
