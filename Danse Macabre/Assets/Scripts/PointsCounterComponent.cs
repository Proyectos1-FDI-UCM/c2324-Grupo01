using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PointsCounterComponent : MonoBehaviour
{
    #region parameters
    private double _totalPoint = 0f;
    private double _basicPoint = 0f;
    #endregion

    #region references
    private TextMeshProUGUI _textPuntos;
    #endregion

    #region methods
    public void AddPoints(int points)
    {
        _totalPoint += points;
    }
    #endregion
    void Start()
    {
        _textPuntos = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {

        _basicPoint += Time.deltaTime;
        _totalPoint += Time.deltaTime;
        _textPuntos.text = _totalPoint.ToString("0");
    }
}
