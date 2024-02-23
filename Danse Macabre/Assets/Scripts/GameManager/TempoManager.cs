using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TempoManager : MonoBehaviour
{
    [SerializeField]
    int BPM = 190; // BPM de la canción del nivel. Serializado
    [SerializeField]
    float TilesPerTick; // Cantidad de tiles que hay que recorrer en cada tick de la canción. Serializado

    public float SecondsPerTick; // Cantidad de tiempo para completar un tick. Referenciable
    public float PlayerSpeed; // Velocidad del jugador calculada basado en BPM y TilesPerTick. Referenciable

    void Start()
    {

        SecondsPerTick = 60f / BPM;
        PlayerSpeed = TilesPerTick / SecondsPerTick;
        Debug.Log("SPT: " + SecondsPerTick + " / PlayerSpeed: " + PlayerSpeed);

        //StartCoroutine(Tempo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    IEnumerator Tempo()
    {
        int counter = 0;
        while (true)
        {
            Debug.Log("TICK: " + counter + " / SecondsPerTick: " + SecondsPerTick + " / Time.time: " + Time.time);
            yield return new WaitForSeconds(SecondsPerTick);
            counter++;
        }
    }
    */
}
