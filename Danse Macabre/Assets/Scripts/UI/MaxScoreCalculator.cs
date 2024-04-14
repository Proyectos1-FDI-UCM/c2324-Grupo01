using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MaxScoreCalculator : MonoBehaviour
{
    #region references
    private ComboManager _comboManager;
    private ScoreManager _scoreManager;
    #endregion
    #region properties
    private float _nCoins;
    private float _nDashCoins;
    private float _nEnemy;
    private float _nBox;
    private float _nArrow;

    private float _coinsValue;
    private float _dashCoinValue;
    private float _enemyValue;
    private float _boxValue;

    private double _basicScore;
    private double _scoreCombo;
    private double _maxScore;
    #endregion

    #region methods

    private static MaxScoreCalculator instance;
    public static MaxScoreCalculator Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    //Codigo Haosahuang
    public void ObjectRegister(int type, int value) //se registran los objetos de la escena
    {
        //tipo 0=monedas, 1=enemigo, 2=box, 3=DashCoin
        if (type == 0)
        {
            _nCoins++;
            _coinsValue = value;
        }
        else if (type == 1)
        {
            _nEnemy++;
            _enemyValue = value;
        }
        else if (type == 2)
        {
            _nBox++;
            _boxValue = value;
        }
        else if (type == 3)
        {
            _nDashCoins++;
            _dashCoinValue = value;
        }
    }

    public void ArrowRegister() //el numero de flechas
    {
        _nArrow++;
    }
    public void ScoreCalculate() //calcula el numero maximo de score que se puede obtener
    {
        //puntuacion sin combo
        _basicScore = _nCoins * _coinsValue
                    + _nDashCoins * _dashCoinValue
                    + _nEnemy * _enemyValue
                    + _nBox * _boxValue
                    + _nArrow * _scoreManager.perfectTimingValue;

        //punctuacion con combo
        _scoreCombo = (_basicScore - _comboManager.threshold3) * _comboManager.threshold3mul
                        + _comboManager.threshold3 - _comboManager.threshold2 * _comboManager.threshold2mul
                        + _comboManager.threshold2 - _comboManager.threshold1 * _comboManager.threshold1mul
                        + _comboManager.threshold1;

        //puntuacion con los puntos del tiempo transcurrido
        _maxScore = _scoreCombo + _scoreManager._basicPoint;
    }
    public void SaveSceneMaxScore() //guarda la puntuacion maxima de la escena
    {
        ScoreCalculate();
        PlayerPrefs.SetFloat("MaxScore", (float)_maxScore);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _comboManager=FindObjectOfType<ComboManager>();
        _scoreManager=FindObjectOfType<ScoreManager>();
    }
    void Update()
    {
        ScoreCalculate();
        Debug.Log("MaxScore"+ _maxScore);
    }
}
