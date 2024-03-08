using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    private bool isPausado = false;

    public void Pausa() 
    {
        if (isPausado) {
            Reanudar();
        }
        else {
            Time.timeScale = 0f;   //Para detener el juego
            MusicManager.Instance.StopPlayingSong();
            menuPausa.SetActive(true);
            isPausado = true;
        }

    }
    public void Reanudar() 
    { 
        Time.timeScale = 1f;    //Para arrancar el juego
        MusicManager.Instance.ResumePlayingSong();
        menuPausa.SetActive(false);
        isPausado = false;
    }
    public void Reiniciar() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   //Reiniciar la escena que est�
    }
    public void Quit() 
    {
        SceneManager.LoadScene(0);
    }
}
