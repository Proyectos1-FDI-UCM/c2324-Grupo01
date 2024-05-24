using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    private CheckpointManager _checkpointManager;
    [SerializeField] private GameObject menuPausa;
    private bool isPausado = false;

    private void Start()
    {
        _checkpointManager = GetComponent<CheckpointManager>();
    }
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
        _checkpointManager.ResetCheckpoint();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   //Reiniciar la escena que est�
    }
    public void Quit() 
    {
        _checkpointManager.ResetCheckpoint();
        PlayerPrefs.SetInt("MenuLevelActivo", 1);
        SceneManager.LoadScene(0);
    }
}
