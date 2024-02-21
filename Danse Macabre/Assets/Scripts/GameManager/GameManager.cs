using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject menuMuerte;

    public void Muerte()
    {
        Time.timeScale = 0f;
        menuMuerte.SetActive(true);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
