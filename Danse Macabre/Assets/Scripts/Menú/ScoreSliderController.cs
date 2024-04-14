using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSliderController : MonoBehaviour
{
    #region references
    private MenuFinalJuego menuFinalJuego;

    private Slider _mySlider;
    #endregion

    #region properties
    private float _value;
    #endregion

    #region methods
    public void SetProgress()
    {
        float playerScore = PlayerPrefs.GetFloat("FinalScore", 0f);
        float MaxScore = PlayerPrefs.GetFloat("MaxScore", 0f);
        float progress = playerScore / MaxScore;

        _value = Mathf.Clamp01(progress);
        _mySlider.value = 1 - _value;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _mySlider = GetComponent<Slider>();
        menuFinalJuego = GetComponent<MenuFinalJuego>();
    }
    private void Update()
    {
        SetProgress();
        //Debug.Log("progress: "+ _mySlider.value);
    }
}
