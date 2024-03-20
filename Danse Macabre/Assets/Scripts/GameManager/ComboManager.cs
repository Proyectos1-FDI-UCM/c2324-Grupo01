using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private float combo = 0;
    public float multiplier = 0;

    [SerializeField]
    private float threshold1 = 100; // Threshold de multiplicador
    [SerializeField]
    private float threshold1mul = 1.1f; // Multiplicador correspondiente al threshold anterior
    [SerializeField]
    private float threshold2 = 200; // Threshold de multiplicador
    [SerializeField]
    private float threshold2mul = 1.2f; // Multiplicador correspondiente al threshold anterior
    [SerializeField]
    private float threshold3 = 300; // Threshold de multiplicador
    [SerializeField]
    private float threshold3mul = 1.3f; // Multiplicador correspondiente al threshold anterior

    void Update()
    {
        // Procesar multiplicador
        if (combo < threshold1)
        {
            multiplier = 1;
        }
        else if (combo >= threshold3)
        {
            multiplier = threshold3mul;
        }
        else if (combo >= threshold2)
        {
            multiplier = threshold2mul;
        }
        else if (combo >= threshold1)
        {
            multiplier = threshold1mul;
        }
        
        Debug.Log("Combo: " + Math.Round(combo) + " | Multiplier: " + multiplier);
    }
    public void addCombo(float n)
    {
        combo = combo + n;
    }
}
