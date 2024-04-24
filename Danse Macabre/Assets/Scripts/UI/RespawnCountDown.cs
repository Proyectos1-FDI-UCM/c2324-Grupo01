using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;

public class RespawnCountDown : MonoBehaviour
{


    private int c;
    [SerializeField] private TextMeshProUGUI _Count;
    [SerializeField] private Canvas _Canvas;
    // Start is called before the first frame update
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
            Invoke("CountDown", 0.66f);
        }
        else
        {
            _Canvas.enabled = false;
        }
    }
}
