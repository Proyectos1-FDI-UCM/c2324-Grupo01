using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVictoria : MonoBehaviour
{
    public void Victoria() 
    {
        SceneManager.LoadScene(0);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovementComponent>()) 
        {
            SceneManager.LoadScene(4);
        }
    }
}
