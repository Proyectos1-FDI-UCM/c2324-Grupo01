using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVictoria : MonoBehaviour
{
    #region properties
    //public bool _isVictory = false;
    #endregion
    #region references
    [SerializeField]
    private TextMeshProUGUI _textPuntuacionFinal;
    #endregion
    public void Victoria()
    {
        SceneManager.LoadScene(0);
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
