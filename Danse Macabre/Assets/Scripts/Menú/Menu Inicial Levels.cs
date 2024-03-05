using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialLevels : MonoBehaviour
{
    public void Jugar_Tutorial() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void Jugar_Nivel1() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    public void Jugar_Nivel2() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }
}
