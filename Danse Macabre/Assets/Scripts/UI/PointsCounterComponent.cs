using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsCounterComponent : MonoBehaviour
{
    #region parameters
    private double _totalPoint = 0f;
    private double _basicPoint = 0f;

    private int _nCoins = 0;
    #endregion

    #region references
    [SerializeField]
    private TextMeshProUGUI _textPuntos;
    #endregion

    #region methods
    public void AddPoints(int points)
    {
        _totalPoint += points;
    }
    
    public void CoinRegister()
    {
        _nCoins++;
    }
    #endregion
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        _basicPoint += Time.deltaTime;
        _totalPoint += Time.deltaTime;
        _textPuntos.text = _totalPoint.ToString("0");
    }
}
