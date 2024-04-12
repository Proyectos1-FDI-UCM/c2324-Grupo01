using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBeatComponent : MonoBehaviour
{
    private ComboManager _combo;
    private TempoManager _tempo;
    [SerializeField]
    private Image _image;

    // Colores correspondientes a cada multiplicador.
    [SerializeField]
    Color Multiplier1Color;
    [SerializeField]
    Color Multiplier2Color;
    [SerializeField]
    Color Multiplier3Color;
    [SerializeField]
    Color Multiplier4Color;

    // Valores del 1 al 0 correspondientes a cada multiplicador. Cuanto más cerca del 1, más visible será
    [SerializeField]
    float Multiplier1Intensity = 0.25f;
    [SerializeField]
    float Multiplier2Intensity = 0.50f;
    [SerializeField]
    float Multiplier3Intensity = 0.75f;
    [SerializeField]
    float Multiplier4Intensity = 1f;

    [SerializeField]
    float BeatDurationMultiplier = 0.5f; // Multiplicador a la duración total de cada iteración del efecto (multiplica a Time.deltaTime)

    Color targetColor;
    float alpha;
    float currentAlpha;
    bool startBeating = false;

    bool toggle = true;

    // Start is called before the first frame update
    void Start()
    {
        _combo = FindObjectOfType<ComboManager>();
        _tempo = FindObjectOfType<TempoManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_combo.multiplier < 2)
        {
            alpha = Multiplier1Intensity;
            targetColor = new Color(Multiplier1Color.r, Multiplier1Color.g, Multiplier1Color.b, Multiplier1Intensity);
        }
        else if (_combo.multiplier >= 4)
        {
            alpha = Multiplier4Intensity;
            targetColor = new Color(Multiplier4Color.r, Multiplier4Color.g, Multiplier4Color.b, Multiplier4Intensity);
        }
        else if (_combo.multiplier >= 3)
        {
            alpha = Multiplier3Intensity;
            targetColor = new Color(Multiplier3Color.r, Multiplier3Color.g, Multiplier3Color.b, Multiplier3Intensity);
        }
        else if (_combo.multiplier >= 2)
        {
            alpha = Multiplier2Intensity;
            targetColor = new Color(Multiplier2Color.r, Multiplier2Color.g, Multiplier2Color.b, Multiplier2Intensity);
        }

        if (startBeating)
        {
            InvokeRepeating("Beat", 0, _tempo.SecondsPerTick);
            startBeating = false;
        }

        _image.color = new Color(targetColor.r, targetColor.g, targetColor.b, currentAlpha);
        currentAlpha = currentAlpha - Time.deltaTime * BeatDurationMultiplier;
        //Debug.Log(currentAlpha + " " + Time.deltaTime * BeatDurationMultiplier);
    }

    public void StartBeat()
    {
        startBeating = true;
    }

    void Beat()
    {
        //Debug.Log("BEAT");
        currentAlpha = alpha;
    }
}

/*
 for (int i = 0; i < BeatDurationTicks; i++)
        {
            currentAlpha = Mathf.Lerp(alphaGoal, 0, i / BeatDurationTicks);
            _image.color = new Color(BaseColor.r, BaseColor.g, BaseColor.b, alphaGoal);
            Debug.Log(currentAlpha + " " + i);
        }
        _image.color = new Color(BaseColor.r, BaseColor.g, BaseColor.b, 0);
*/
