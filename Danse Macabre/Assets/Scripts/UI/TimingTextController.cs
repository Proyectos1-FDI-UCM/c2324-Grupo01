using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimingTextController : MonoBehaviour
{
    #region references
    [SerializeField] TextMeshPro _timingText;
    #endregion

    #region parameters
    [SerializeField] private float _resetTime=1.0f;
    #endregion

    #region methods
    public void TimingText(string timing)
    {
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
