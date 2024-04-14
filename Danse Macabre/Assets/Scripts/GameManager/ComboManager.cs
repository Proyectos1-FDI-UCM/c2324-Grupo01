using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private float combo = 0;
    public float multiplier = 0;
   
    public float threshold1; // Threshold de multiplicador
    
    public float threshold1mul = 2f; // Multiplicador correspondiente al threshold anterior
    
    public float threshold2 = 200; // Threshold de multiplicador
    
    public float threshold2mul = 3f; // Multiplicador correspondiente al threshold anterior
    
    public float threshold3 = 300; // Threshold de multiplicador

    public float threshold3mul = 4f; // Multiplicador correspondiente al threshold anterior

    //modificado por Bing 
    #region references
    private ComboSliderComponent comboSliderComponent;
    #endregion

    private void Start()
    {
        comboSliderComponent = FindObjectOfType<ComboSliderComponent>();
    }
    void Update()
    {
        // Procesar multiplicador
        if (combo < threshold1)
        {
            multiplier = 1;
        }
        else if (combo >= threshold3 )
        {
            if (multiplier != threshold3mul) multiplier = threshold3mul;
        }
        else if (combo >= threshold2)
        {
            if (multiplier != threshold2mul) multiplier = threshold2mul;
        }
        else if (combo >= threshold1)
        {
            if (multiplier != threshold1mul) multiplier = threshold1mul;
        }
        comboSliderComponent.ChangeColor(multiplier);
        //para el slider del combo
        comboSliderComponent.SetPoint(combo);
        //Debug.Log("Combo: " + Math.Round(combo) + " | Multiplier: " + multiplier);
    }
    public void addCombo(float n)
    {
        combo = combo + n;
    }

    public void resetCombo()
    {
        combo = 0;
    }
    
}