using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;

public class RespawnCountDown : MonoBehaviour
{
    #region parameters
    private static float respawnTickSeconds = 0.66f;
    private static float respawnTimeSeconds = 3 * respawnTickSeconds;
    #endregion

    #region properties
    public float RespawnTime { get { return respawnTimeSeconds;}}
    #endregion

    private int c;
    [SerializeField] private TextMeshProUGUI _Count;
    [SerializeField] private Canvas _Canvas;
    
    private void Awake() {
        _Canvas.enabled = false;
    }
    void Start()
    {
        SetCount();
    }

    public void SetCount()
    {
        _Canvas.enabled = true;
        c = 3;
        CountDown();
    }

    private void CountDown()
    {
        if (c > 0)
        {
            _Count.text = c.ToString();
            //Debug.Log("c: " + c + "  " + Time.time);
            c--;
            Invoke("CountDown", respawnTickSeconds);
        }
        else
        {
            _Canvas.enabled = false;
        }
    }
}
