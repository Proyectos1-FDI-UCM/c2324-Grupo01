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
    private int _progressPercent;
    #endregion

    #region methods
    // M�todo para actualizar el valor del Slider.
    private void SetProgress()
    {
        float progress = (_endTransform.position.x - _playerTransform.position.x)/(_endTransform.position.x-_startTransform);
        
        _value = Mathf.Clamp01(progress);
        _mySlider.value = 1-_value;
        if (_mySlider.value <= 1 && _mySlider.value >=0 )
        {
            _progressPercent = (int)Mathf.Round(_mySlider.value * 100f);
        }
        else if (_mySlider.value<0)
        {
            _progressPercent=0;
        }
        else
        {
            _progressPercent = 100;
        }
        progressPercent.text =_progressPercent+ "%";

    }
    public void SaveProgess()
    {
        SetProgress();
        PlayerPrefs.SetFloat("Progress", (float)_value);
        PlayerPrefs.SetInt("ProgressPercent",(int) _progressPercent);
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
