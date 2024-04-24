using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DeathFilterColor : MonoBehaviour
{
    [SerializeField]
    private Image _DeathFilter;

    private int c;

    [SerializeField]
    private Color[] Color;
    // Start is called before the first frame update
    void Start()
    {
        c = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ColorChange()
    {
        

        if(c < 4) //cycles through 4 different colors. can be changed if you add more colors just increasing the counter but time needs to be adjusted manually
        {
            _DeathFilter.color = Color[c];
            //Debug.Log("c: " + c + "  " + Time.time);
            c++;
            Invoke("ColorChange", 0.3f); //change every 0.3 seconds
        }
        


        /*
        _DeathFilter.color = Color[0];
        Debug.Log("1 color");
        Thread.Sleep(500); // Pausa de 0.5 segundos
        _DeathFilter.color = Color[1];
        Debug.Log("2 color");
        Thread.Sleep(500); // Pausa de 0.5 segundos
        _DeathFilter.color = Color[2];
        Debug.Log("3 color");
        Thread.Sleep(500); // Pausa de 0.5 segundos
        _DeathFilter.color = Color[3];
        Debug.Log("4 color");
        Thread.Sleep(500); // Pausa de 0.5 segundos
        */
    }
}
