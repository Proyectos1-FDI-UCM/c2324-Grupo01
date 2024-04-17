using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ComboSliderComponent : MonoBehaviour
{
    #region parameters
    //por defecto, viene x1
    private float previousMul = 1f;

    [SerializeField]
    private float combo1Vol = 1f;
    [SerializeField]
    private float combo2Vol = 1f;
    [SerializeField]
    private float combo3Vol = 1f;
    [SerializeField]
    private float failedComboVol = 1f;
    #endregion
    #region references
    [SerializeField]
    private Image backgroundRender;
    [SerializeField]
    private Image fillRender;
    private Slider slider;
    [SerializeField]
    private Color[] bgColors = new Color[4];
    [SerializeField]
    private Color[] fillColors = new Color[4];
    private ComboManager comboManager;

    [SerializeField]
    private TextMeshProUGUI textoMul;

    [SerializeField]
    private AudioClip comboSound1;
    [SerializeField]
    private AudioClip comboSound2;
    [SerializeField]
    private AudioClip comboSound3;
    [SerializeField]
    private AudioClip comboFailedSound;
    #endregion

    #region properties
    //el valor actual del slider, que llevara al slider.value;
    private float actualValue;

    #endregion
    #region methods
    public void ChangeColor(float mul)
    {
        //si el multiplicador previo es distinto, entonces si cambio el valor
        if (previousMul != mul)
        {
            previousMul = mul;
            //x1.0 (default)
            if (mul - 1f < 0.1f)
            {
                backgroundRender.color = bgColors[0];
                fillRender.color = fillColors[0];
                slider.maxValue = comboManager.threshold1;
                MusicManager.Instance.PlaySoundEffect(comboFailedSound, failedComboVol);

            }
            //x2
            else if (mul-comboManager.threshold1mul < 0.1f)
            {
                backgroundRender.color = bgColors[1];
                fillRender.color = fillColors[1];
                slider.maxValue = comboManager.threshold2;
                MusicManager.Instance.PlaySoundEffect(comboSound1, combo1Vol);
            }
            //x3
            else if (mul - comboManager.threshold2mul < 0.1f)
            {
                backgroundRender.color = bgColors[2];
                fillRender.color = fillColors[2];
                slider.maxValue = comboManager.threshold3;
                MusicManager.Instance.PlaySoundEffect(comboSound2, combo2Vol);
            }
            //x4
            else if (mul - comboManager.threshold3mul < 0.1f)
            {
                backgroundRender.color = bgColors[3];
                fillRender.color = fillColors[3];
                MusicManager.Instance.PlaySoundEffect(comboSound3, combo3Vol);
            }
            
            ResetCursor();
            textoMul.text = $"X{mul}";
        }
        
    }
    private void ResetCursor()
    {
        slider.value = 0f;
    }
    public void SetPoint(double value)
    {
        actualValue = Mathf.Clamp((float)value, 0, slider.maxValue);
        slider.value = actualValue;

    }
    #endregion
    private void Start()
    {
        slider = GetComponent<Slider>();
        //fillRender = slider.fillRect.GetComponentInChildren<Image>();
        comboManager = FindObjectOfType<ComboManager>();
        slider.maxValue = comboManager.threshold1;
    }
}
