using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TempoManager : MonoBehaviour
{
    [SerializeField]
    int BPM = 60; // BPM de la canci�n del nivel. Serializado
    [SerializeField]
    float TilesPerTick = 4; // Cantidad de tiles que hay que recorrer en cada tick de la canci�n. Serializado

    public float SecondsPerTick; // Cantidad de tiempo para completar un tick. Referenciable
    public float PlayerSpeed; // Velocidad del jugador calculada basado en BPM y TilesPerTick. Referenciable

    void Awake()
    {
        SecondsPerTick = 60f / BPM;
        PlayerSpeed = TilesPerTick / SecondsPerTick;
        Debug.Log("SPT: " + SecondsPerTick + " / PlayerSpeed: " + PlayerSpeed);
    }
}
