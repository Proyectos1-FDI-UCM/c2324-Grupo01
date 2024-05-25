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
    private ScreenBeatComponent screenBeatComponent;

    private bool cambioColor=false;
    private float sumThreshold = 0;
    
    #endregion

    private void Start()
    {
        comboSliderComponent = FindObjectOfType<ComboSliderComponent>();
        screenBeatComponent = FindAnyObjectByType<ScreenBeatComponent>();
    }
    void Update()
    {
        if (combo >= threshold1 && !screenBeatComponent.Combo) screenBeatComponent.Combo = true;
        else if (combo < threshold1 && screenBeatComponent.Combo) screenBeatComponent.Combo = false;
        
        // Procesar multiplicador
        if (combo < threshold1)
        {
            multiplier = 1;
            cambioColor = true;
            sumThreshold = 0;
        }
        else if (combo - (threshold1+threshold2) >= threshold3)
        {
            if (multiplier != threshold3mul) 
            {
                multiplier = threshold3mul;
                sumThreshold = threshold3 + threshold2 + threshold1;
                cambioColor = true;
            }
        }
        else if (combo - threshold1 >= threshold2)
        {
            if (multiplier != threshold2mul)
            { 
                multiplier = threshold2mul;
                sumThreshold = threshold2 + threshold1;
                cambioColor = true;
            }
        }
        else if (combo >= threshold1)
        {
            if (multiplier != threshold1mul)
            {
                multiplier = threshold1mul;
                sumThreshold = threshold1;
                cambioColor = true;
            }
        }
        if (cambioColor)
        {
            comboSliderComponent.ChangeColor(multiplier);
            //para el slider del combo

            //Debug.Log("Combo: " + Math.Round(combo) + " | Multiplier: " + multiplier);
        }
        comboSliderComponent.SetPoint(combo-sumThreshold);
        cambioColor = false;
    }
    public void addCombo(float n)
    {
        combo = combo + n;
    }

    public void resetCombo()
    {
        combo = 0;
    }
    /*
    private void FixedUpdate()
    {
        //Debug.Log("Combo: " + (combo) + " Sum: " + sumThreshold);
    }
    */
}