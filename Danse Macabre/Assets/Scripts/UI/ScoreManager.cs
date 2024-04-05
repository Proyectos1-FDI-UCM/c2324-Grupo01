using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    #region parameters
    private float perfectTimingValue = 10;
    private float greatTimingValue = 5;
    private float goodTimingValue = 1;
    #endregion 

    #region references
    [SerializeField] private TextMeshProUGUI _textPuntos;
    [SerializeField] private TextMeshPro _textPuntosAñadidos;
    [SerializeField] private TextMeshProUGUI _textoArriba;

    private ComboSliderComponent comboSliderComponent;
    private ComboManager _combo;
    #endregion

    #region properties
    //para el slider del combo
    public double _totalPoint = 0f;
    private double _basicPoint = 0f;
    private float _coinPoint = 0;
    private float _enemyPoint = 0;
    private float _destroyObjectPoint = 0;
    private int _nCoins = 0;
    private float _timingPoints = 0;

    private float _addPoints = 0; //los puntos que se van sumando
    private float _sudPoints= 0;
    private float _lastPickupTime; 
    [SerializeField] private float _resetTime = 0.3f;

    #endregion

    #region methods
    public void AddPoints(float points, int type)
    {
        //tipo de punto, 0=monedas, 1=enemigo, 2=objeto
        if (type==0) _coinPoint += (points * _combo.multiplier);
        else if (type==1) _enemyPoint += (points * _combo.multiplier);
        else if(type==2) _destroyObjectPoint += (points * _combo.multiplier);

        if (points < 0) // si es negativo
        {
            _addPoints = 0;
            _sudPoints += points;
            _textPuntosAñadidos.text = _sudPoints.ToString();
            _combo.resetCombo();
        }
        else
        {
            _addPoints += (points * _combo.multiplier);
            _textPuntosAñadidos.text = "+" + _addPoints.ToString(); //poner +numero
            _combo.addCombo(points);
        }
        _lastPickupTime = Time.time;
    }
    private void ResetText()
    {
        _textPuntosAñadidos.text = " "; //quitar el texto
        AddToTotalPoint();
    }
    void AddToTotalPoint()
    {
        float points;
        if (_addPoints > 0) //poner el texto arriba cuando lleva un tiempo sin coger nada
        {
            points = _addPoints;          
            _totalPoint += _addPoints;
            _textoArriba.text ="+"+ points.ToString();
        }
        else
        {
            points = _sudPoints;
            _totalPoint += _sudPoints;
            _textoArriba.text = points.ToString();
        }
        Invoke("ResetUpText", 0.4f);
    }
    private void ResetUpText()
    {
        _textoArriba.text = "   ";
    }
    public void AddTimingPoints(string timing)
    {
        if (timing == "PERFECT") {
            _timingPoints += (perfectTimingValue * _combo.multiplier);
            _totalPoint += (perfectTimingValue * _combo.multiplier);
            _combo.addCombo(perfectTimingValue);
        }
        else if (timing == "GREAT") {
            _timingPoints += (greatTimingValue * _combo.multiplier);
            _totalPoint += (greatTimingValue * _combo.multiplier);
            _combo.addCombo(greatTimingValue);
        }
        else if (timing == "GOOD") {
            _timingPoints += (goodTimingValue * _combo.multiplier);
            _totalPoint += (goodTimingValue * _combo.multiplier);
            _combo.addCombo(goodTimingValue);
        }
        else if (timing == "WRONG")
        {
            _combo.resetCombo();
        }
        else if (timing == "MISSED")
        {
            //_combo.resetCombo();
        }
    }

    public void CoinRegister()
    {
        _nCoins++;
    }

    public void SaveCheckpointScore()
    {
        PlayerPrefs.SetFloat("CheckpointScore", (float)_totalPoint);
        PlayerPrefs.SetFloat("CheckpointCoinScore", (float)_coinPoint);
        PlayerPrefs.SetFloat("CheckpointEnemyScore", (float)_enemyPoint);
        PlayerPrefs.SetFloat("CheckpointObjectScore", (float)_destroyObjectPoint);
    }

    public void LoadCheckpointScore()
    {
        _totalPoint = PlayerPrefs.GetFloat("CheckpointScore");
        _coinPoint = PlayerPrefs.GetFloat("CheckpointCoinScore");
        _enemyPoint = PlayerPrefs.GetFloat("CheckpointEnemyScore");
        _destroyObjectPoint = PlayerPrefs.GetFloat("CheckpointObjectScore");
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
        _textoArriba.enabled = false;
        _combo = FindObjectOfType<ComboManager>();
        comboSliderComponent =FindObjectOfType<ComboSliderComponent>();
    }
    void Update()
    {
        _basicPoint += Time.deltaTime;
        _totalPoint += Time.deltaTime;

        //para el slider del combo
        comboSliderComponent.SetPoint(_totalPoint);
        //Debug.Log("Puntos" + _totalPoint);
        _textPuntos.text = _totalPoint.ToString("0");


        // cuando lleva un tiempo sin coger objeto se resetea
        if (Time.time - _lastPickupTime >= _resetTime) 
        {
            _textoArriba.enabled = true;
            if(_addPoints>0||_sudPoints<0)ResetText();
            _addPoints = 0;
            _sudPoints = 0;
        }
    }
}
