using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    #region parameters
    private int perfectTimingValue = 10;
    private int goodTimingValue = 5;
    private int badTimingValue = 1;
    #endregion 

    #region references
    [SerializeField]
    private TextMeshProUGUI _textPuntos;
    #endregion

    #region properties
    private double _totalPoint = 0f;
    private double _basicPoint = 0f;
    private int _coinPoint = 0;
    private int _enemyPoint = 0;
    private int _destroyObjectPoint = 0;
    private int _nCoins = 0;
    private int _timingPoints = 0;
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
    public void AddTimingPoints(string timing)
    {
        if (timing == "PERFECT") {
            _timingPoints += perfectTimingValue;
            _totalPoint += perfectTimingValue;
        }
        else if (timing == "GOOD") {
            _timingPoints += goodTimingValue;
            _totalPoint += goodTimingValue;
        }
        else if (timing == "BAD") {
            _timingPoints += badTimingValue;
            _totalPoint += badTimingValue;
        }
        else if (timing == "WRONG") { /*lo dejo por si acaso*/ }
        else if (timing == "MISSED") { /*lo dejo por si acaso*/ }
    }

    public void CoinRegister()
    {
        _nCoins++;
    }
    public void SaveFinalScore() 
    {
        // Guarda la puntuaci�n en PlayerPrefs antes de cambiar de escena
        PlayerPrefs.SetFloat("FinalScore", (float)_totalPoint);
    }
    #endregion

    void Update()
    {
        _basicPoint += Time.deltaTime;
        _totalPoint += Time.deltaTime;
        //Debug.Log("Puntos" + _totalPoint);
        _textPuntos.text = _totalPoint.ToString("0");
    }
}
