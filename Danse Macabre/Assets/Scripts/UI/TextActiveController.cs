using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class TextActiveController : MonoBehaviour
{
    [SerializeField]
    private GameObject text;//el text

    [SerializeField]
    private float duration = 1.0f; // tiempo que aparece en la pantalla;

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
            Invoke("HideText", duration);

        }
    } 
    private void HideText()
    {
        text.SetActive(false);
    }
    
}
