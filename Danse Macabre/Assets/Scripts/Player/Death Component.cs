using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject menuMuerte;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MovementComponent>()) 
        {
            Time.timeScale = 0f;
            menuMuerte.SetActive(true);
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
