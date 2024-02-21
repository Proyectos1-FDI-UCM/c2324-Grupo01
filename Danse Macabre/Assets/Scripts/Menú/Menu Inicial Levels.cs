using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialLevels : MonoBehaviour
{
    public void Jugar_Tutorial() 
    {
        SceneManager.LoadScene(1);
    }
    public void Jugar_Nivel1() 
    {
        SceneManager.LoadScene(2);
    }
    public void Jugar_Nivel2() 
    {
        SceneManager.LoadScene(3);
    }
    public void Jugar_Kai_Muerte()   //Provisional
    {
        SceneManager.LoadScene(4);
    }

}
