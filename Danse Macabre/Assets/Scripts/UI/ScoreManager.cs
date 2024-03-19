using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    #region parameters
    private int perfectTimingValue = 10;
    private int goodTimingValue = 5;
    private int badTimingValue = 1;
    #endregion 

    #region references
    [SerializeField] private TextMeshProUGUI _textPuntos;
    [SerializeField] private TextMeshProUGUI _textPuntosAñadidos;
    #endregion

    #region properties
    private double _totalPoint = 0f;
    private double _basicPoint = 0f;
    private int _coinPoint = 0;
    private int _enemyPoint = 0;
    private int _destroyObjectPoint = 0;
    private int _nCoins = 0;
    private int _timingPoints = 0;

    private int _addPoints = 0; //los puntos que se van sumando
    private int _sudPoints= 0;
    private float _lastPickupTime; 
    [SerializeField] private float _resetTime = 0.5f;

    #endregion

    #region methods
    public void AddPoints(int points, int type)
    {
        //tipo de punto, 0=monedas, 1=enemigo, 2=objeto
        if (type==0) _coinPoint += points;
        else if (type==1) _enemyPoint += points;
        else if(type==2) _destroyObjectPoint += points;

        _totalPoint += points;
        if (points < 0) // si es negativo
        {
            _addPoints = 0;
            _sudPoints += points;
            _textPuntosAñadidos.text = _sudPoints.ToString();
        }
        else
        {
            _addPoints += points;
            UpdateText();
        }
        _lastPickupTime = Time.time;
    }
    private void ResetText()
    {
        _textPuntosAñadidos.text = " "; //quitar el texto
    }
    void UpdateText()
    {
        _textPuntosAñadidos.text = "+" + _addPoints.ToString(); //poner +numero
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
        PlayerPrefs.SetFloat("CoinScore", (float)_coinPoint);
        PlayerPrefs.SetFloat("EnemyScore", (float)_enemyPoint);
        PlayerPrefs.SetFloat("ObjectScore", (float)_destroyObjectPoint);
    }
    #endregion
    void Start()
    {
        _lastPickupTime = Time.time;
    }
    void Update()
    {
        _basicPoint += Time.deltaTime;
        _totalPoint += Time.deltaTime;
        //Debug.Log("Puntos" + _totalPoint);
        _textPuntos.text = _totalPoint.ToString("0");


        // cuando lleva un tiempo sin coger objeto se resetea
        if (Time.time - _lastPickupTime >= _resetTime) 
        {
            _addPoints = 0;
            _sudPoints = 0;
            ResetText();
        }
    }
}
