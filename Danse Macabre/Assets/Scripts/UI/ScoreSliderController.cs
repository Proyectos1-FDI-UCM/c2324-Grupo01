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
    
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _sliderEndSound;

    #endregion

    #region properties
    private float _targetValue;
    private float _playerScore;
    private float _MaxScore;
    private float progress;
    private float newValue;
    private bool reproducido = false;
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
        _targetValue = Mathf.Clamp01(_playerScore / (_MaxScore * 0.8f));
        _audioSource = FindObjectOfType<AudioSource>();
    }
    private void Update()
    {
        //Debug.Log("valor" + _targetValue);
        progress= Mathf.Lerp(progress, _targetValue, sliderTime*Time.deltaTime);
        
        _mySlider.value = progress;
       
        if (_targetValue -_mySlider.value < 0.01f && !reproducido)
        {
            _audioSource.PlayOneShot(_sliderEndSound, 0.6f);
            reproducido= true;
        }
    }
}
