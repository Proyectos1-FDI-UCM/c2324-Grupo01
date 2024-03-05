using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    #region parameters
    private double _totalPoint = 0f;
    private double _basicPoint = 0f;
    private int _coinPoint = 0;
    private int _enemyPoint = 0;
    private int _destroyObjectPoint=0;

    private int _nCoins = 0;
    #endregion

    #region references
    [SerializeField]
    private TextMeshProUGUI _textPuntos;
    private MenuVictoria _victoria;
    #endregion

    #region methods
    public void AddCoinPoints(int points)
    {
        _coinPoint+= points;
        _totalPoint+= points;
    }
    public void AddEnemyPoints(int points)
    {
        _enemyPoint += points;
        _totalPoint += points;
    }
    public void AddObjectPoints(int points)
    {
        _destroyObjectPoint += points;
        _totalPoint += points;
    }

    public void CoinRegister()
    {
        _nCoins++;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovementComponent>())
        {
            // Guarda la puntuación en PlayerPrefs antes de cambiar de escena
            PlayerPrefs.SetFloat("FinalScore", (float)_totalPoint);
            //Cambiar escena de Victoria
            SceneManager.LoadScene(4);
        }
    }
    #endregion
    void Update()
    {
        _basicPoint += Time.deltaTime;
        _totalPoint += Time.deltaTime;
        _textPuntos.text = _totalPoint.ToString("0");
    }
}
