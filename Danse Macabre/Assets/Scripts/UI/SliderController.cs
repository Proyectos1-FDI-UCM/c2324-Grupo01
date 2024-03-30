using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    
    #region references
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _endTransform;
    [SerializeField]
    private float _startTransform;
    [SerializeField]
    private TextMeshProUGUI progressPercent;

    private Slider _mySlider;
    #endregion

    #region properties
    private float _value;
    #endregion

    #region methods
    // Método para actualizar el valor del Slider.
    public void SetProgress()
    {
        float progress = (_endTransform.position.x - _playerTransform.position.x)/(_endTransform.position.x-_startTransform);
        
        _value = Mathf.Clamp01(progress);
        _mySlider.value = 1-_value;
        if (_mySlider.value <= 1 && _mySlider.value >=0 )
        {
            progressPercent.text = Mathf.Round(_mySlider.value * 100f).ToString() + "%";
        }
        else if (_mySlider.value<0)
        {
            progressPercent.text = "0%";
        }
        else
        {
            progressPercent.text = "100%";
        }
        
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _mySlider = GetComponent<Slider>();
    }
    private void Update()
    {
        SetProgress();
        //Debug.Log("progress: "+ _mySlider.value);
    }
}
