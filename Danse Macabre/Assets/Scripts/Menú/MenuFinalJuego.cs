using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinalJuego : MonoBehaviour
{
    #region references
    [SerializeField]
    private TextMeshProUGUI _textPuntuacionFinal;
    [SerializeField]
    private TextMeshProUGUI _textPuntuacionMoneda;
    [SerializeField]
    private TextMeshProUGUI _textPuntuacionEnemigo;
    [SerializeField]
    private TextMeshProUGUI _textPuntuacionObjecto;
    #endregion
    #region properties
    //public bool quitToMenuLevel = false;
    #endregion
    public void QuitMenuFinal()
    {
        GameManager.Instance.ResetCheckpoint();

        //Una bandera para cuando le damos al quit ir directamente al menú de niveles
        PlayerPrefs.SetInt("MenuLevelActivo", 1);
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        //Obtiene el nombre de la escena anterior
        string sceneName = PlayerPrefs.GetString("PreviousScene", "DefaultSceneName");
        //Carga la escena anterior
        GameManager.Instance.ResetCheckpoint();
        SceneManager.LoadScene(sceneName);
    }
    void Start()
    {
        // Verifica si la puntuaci�n final est� disponible en PlayerPrefs
        if (PlayerPrefs.HasKey("FinalScore"))
        {
            // Carga la puntuaci�n guardada desde PlayerPrefs y la muestra
            float finalScore = PlayerPrefs.GetFloat("FinalScore", 0f);
            float coinScore = PlayerPrefs.GetFloat("CoinScore", 0f);
            float enemyScore = PlayerPrefs.GetFloat("EnemyScore", 0f);
            float objectScore = PlayerPrefs.GetFloat("ObjectScore", 0f);
            _textPuntuacionFinal.text = finalScore.ToString("0");
            _textPuntuacionMoneda.text = coinScore.ToString("0");
            _textPuntuacionEnemigo.text = enemyScore.ToString("0");
            _textPuntuacionObjecto.text = objectScore.ToString("0");
        }
    }
}
