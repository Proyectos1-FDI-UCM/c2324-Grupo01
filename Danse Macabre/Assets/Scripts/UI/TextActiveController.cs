using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextActiveController : MonoBehaviour
{
    [SerializeField]
    private GameObject text;//el text
    [SerializeField]
    private float displayTime = 4f; // Tiempo en segundos para mostrar el texto

    void Start()
    {
        text.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MovementComponent player = collision.GetComponent<MovementComponent>();
        if (player != null) //cuando el jugador colisiona activa el texto
        {
            text.SetActive(true);
            Invoke("HideText", displayTime); // lo oculta despues de un tiempo determinado
        }
    } 
    private void HideText()
    {
        text.SetActive(false);
    }
}
