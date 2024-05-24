using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ProgressSliderController : MonoBehaviour
{
    #region
    private Slider _progressSlider;
    [SerializeField]private TextMeshProUGUI _Percent;
    #endregion
    private float _playerProgress;
    private float _initial = 0;
    private int _progressPercent;
    [SerializeField]private float _sliderTime=0.5f;
    void Start()
    {
        _progressSlider = GetComponent<Slider>();
        _playerProgress = PlayerPrefs.GetFloat("Progress", 0f);
        _progressPercent = PlayerPrefs.GetInt("ProgressPercent", 0);
    }

    // Update is called once per frame
    void Update()
    {
        _initial = Mathf.Lerp(_initial, 1-_playerProgress, _sliderTime*Time.deltaTime);
        _progressSlider.value = _initial;
        _Percent.text = _progressPercent.ToString() + "%";
        //Debug.Log("Percent"+_progressPercent);
    }
}
