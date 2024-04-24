using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimingTextController : MonoBehaviour
{
    #region references
    [SerializeField] TextMeshPro _timingText;
    [SerializeField] Color[] colors = new Color[5];
    #endregion

    #region parameters
    [SerializeField] private float _resetTime=0.6f;
    private float _time = 0;
    #endregion

    #region methods
    public void TimingText(string timing)
    {
        //Escribe el timing del jugador segun como ha jugado
        if (timing == "PERFECT")
        {
            _timingText.color = colors[0];
            _timingText.text = timing;
        }
        else if (timing == "GREAT")
        {
            _timingText.color = colors[1];
            _timingText.text = timing;
        }
        else if (timing == "GOOD")
        {
            _timingText.color = colors[2];
            _timingText.text = timing;
        }
        else if (timing == "WRONG")
        {
            _timingText.color = colors[3];
            _timingText.text = timing;
        }
        else if (timing == "MISSED")
        {
            _timingText.color = colors[4];
        }
        _timingText.text = timing;
        _time = 0;
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        //contador de tiempo para que desaparezca el texto
        _time+=1*Time.deltaTime;
        if (_time>_resetTime)
        {
            TimingText(" ");
            _time=0;
        }
    }
}
