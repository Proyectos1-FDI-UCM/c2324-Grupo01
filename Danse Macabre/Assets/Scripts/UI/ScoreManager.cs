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
    private float _coinPoint = 0;
    private float _enemyPoint = 0;
    private float _destroyObjectPoint = 0;
    

    private int _nCoins = 0;
    #endregion

    #region references
    [SerializeField]
    private TextMeshProUGUI _textPuntos;
    private MenuVictoria _victoria;
    private ComboManager _combo;
    #endregion

    #region methods
    public void AddCoinPoints(int points)
    {
        _coinPoint+= points * _combo.multiplier;
        _totalPoint+= points * _combo.multiplier;
        float comboPoints = points;
        if (points < 0)
        {
            comboPoints = comboPoints * _combo.comboPenaltyMultiplier;
        }
        _combo.addCombo(comboPoints);
    }
    public void AddEnemyPoints(float points)
    {
        _enemyPoint += points * _combo.multiplier;
        _totalPoint += points * _combo.multiplier;
        float comboPoints = points;
        if (points < 0)
        {
            comboPoints = comboPoints * _combo.comboPenaltyMultiplier;
        }
        _combo.addCombo(comboPoints);
    }
    public void AddObjectPoints(float points)
    {
        _destroyObjectPoint += points * _combo.multiplier;
        _totalPoint += points * _combo.multiplier;
        float comboPoints = points;
        if (points < 0)
        {
            comboPoints = comboPoints * _combo.comboPenaltyMultiplier;
        }
        _combo.addCombo(comboPoints);
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

    void Start()
    {
        _combo = FindObjectOfType<ComboManager>();
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
