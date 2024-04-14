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
    [SerializeField] private float _resetTime=1.0f;
    #endregion

    #region methods
    public void TimingText(string timing)
    {
        if (timing == "PERFECT")
        {
            _timingText.color = colors[0];
        }
        else if (timing == "GREAT")
        {
            _timingText.color = colors[1];
        }
        else if (timing == "GOOD")
        {
            _timingText.color = colors[2];
        }
        else if (timing == "WRONG")
        {
            _timingText.color = colors[3];
        }
        else if (timing == "MISSED")
        {
            _timingText.color = colors[4];
        }
        _timingText.text = timing;
        Invoke("resetText", _resetTime);
    }
    private void resetText()
    {
        _timingText.text = " ";
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }
}
