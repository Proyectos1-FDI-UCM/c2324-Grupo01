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
    //el valor que se resta cuando paso de un slider a otro;
    [SerializeField]
    private float errasedValue = 0f;
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
                errasedValue = 0f;

            }
            //x1.5
            else if (mul-comboManager.threshold1mul < 0.1f)
            {
                backgroundRender.color = bgColors[1];
                fillRender.color = fillColors[1];
                slider.maxValue = comboManager.threshold2;
                errasedValue = comboManager.threshold1;
            }
            //x2
            else if (mul - comboManager.threshold2mul < 0.1f)
            {
                backgroundRender.color = bgColors[2];
                fillRender.color = fillColors[2];
                slider.maxValue = comboManager.threshold3;
                errasedValue = comboManager.threshold2;
            }
            //x3
            else if (mul - comboManager.threshold3mul < 0.1f)
            {
                backgroundRender.color = bgColors[3];
                fillRender.color = fillColors[3];
                errasedValue = comboManager.threshold3;
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
        actualValue = Mathf.Clamp((float)value-errasedValue, 0, slider.maxValue);
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
