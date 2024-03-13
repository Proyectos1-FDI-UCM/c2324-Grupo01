using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerte : MonoBehaviour
{
    #region references
    [SerializeField]
    private TextMeshProUGUI _textPuntuacionFinal;
    #endregion
    public void Muerte()
    {
        SceneManager.LoadScene(0);
    }
    public void RestaurarMenuMuerte()
    {
        //Obtiene el nombre de la escena anterior
        string sceneName = PlayerPrefs.GetString("PreviousScene", "DefaultSceneName");
        //Carga la escena anterior
        SceneManager.LoadScene(sceneName);
    }
    void Start()
    {
        // Verifica si la puntuación final está disponible en PlayerPrefs
        if (PlayerPrefs.HasKey("FinalScore"))
        {
            // Carga la puntuación guardada desde PlayerPrefs y la muestra
            float finalScore = PlayerPrefs.GetFloat("FinalScore", 0f);
            _textPuntuacionFinal.text = finalScore.ToString("0");
        }
    }
}
