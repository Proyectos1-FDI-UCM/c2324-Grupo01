using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSliderController : MonoBehaviour
{
    #region references
    private MenuFinalJuego menuFinalJuego;

    [SerializeField] private float sliderTime=1f;

    private Slider _mySlider;
    #endregion

    #region properties
    private float _targetValue;
    private float _playerScore;
    private float _MaxScore;
    private float progress;
    private float newValue;
    #endregion

    #region methods
    private void Awake()
    {
        _playerScore = PlayerPrefs.GetFloat("FinalScore", 0f);
        _MaxScore = PlayerPrefs.GetFloat("MaxScore", 0f);
    }
    #endregion
    void Start()
    {
        _mySlider = GetComponent<Slider>();
        menuFinalJuego = GetComponent<MenuFinalJuego>();
        _targetValue = Mathf.Clamp01(_playerScore /( _MaxScore * (float)0.8));
    }
    private void Update()
    {
        if(progress<=_targetValue)
        {
           progress= Mathf.Lerp(progress, _targetValue, sliderTime*Time.deltaTime);
        }
        _mySlider.value = progress;
    }
}
