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

    [SerializeField]
    private TextMeshProUGUI _textMaxPuntos;


    [SerializeField]
    private TextMeshProUGUI _textPerfectNumber;
    [SerializeField]
    private TextMeshProUGUI _textGreatNumber;
    [SerializeField]
    private TextMeshProUGUI _textGoodNumber;
    [SerializeField]
    private TextMeshProUGUI _textWrongNumber;
    [SerializeField]
    private TextMeshProUGUI _textMissNumber;

    [SerializeField]
    private TextMeshProUGUI _textPlayerRanking;
    #endregion

    #region properties
    //public bool quitToMenuLevel = false;

    private float _finalScore;
    private float _MaxScore;

    private string _ranking;
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

    private void PlayerRanking()
    {
        if (_finalScore < _MaxScore / 4) _ranking = "C";
        else if (_finalScore < (_MaxScore / 4) * 2) _ranking = "B";
        else if (_finalScore < (_MaxScore / 4) * 3) _ranking = "A";
        else _ranking = "S";
    }
    private void WritePlayerRanking()
    {
        PlayerRanking();
        _textPlayerRanking.text = _ranking;
    }

    private void WriteTimingNumber()
    {
        _textPerfectNumber.text = PlayerPrefs.GetInt("PerfectNumber").ToString();
        _textGreatNumber.text = PlayerPrefs.GetInt("GreatNumber").ToString();
        _textGoodNumber.text = PlayerPrefs.GetInt("GoodNumber").ToString();
        _textMissNumber.text = PlayerPrefs.GetInt("MissNumber").ToString();
        _textWrongNumber.text = PlayerPrefs.GetInt("WrongNumber").ToString();

        _textMaxPuntos.text = ((int)_MaxScore).ToString();
    }
    void Start()
    {
        // Verifica si la puntuaci�n final est� disponible en PlayerPrefs
        if (PlayerPrefs.HasKey("FinalScore"))
        {
            // Carga la puntuaci�n guardada desde PlayerPrefs y la muestra
            _finalScore = PlayerPrefs.GetFloat("FinalScore", 0f);
            float coinScore = PlayerPrefs.GetFloat("CoinScore", 0f);
            float enemyScore = PlayerPrefs.GetFloat("EnemyScore", 0f);
            float objectScore = PlayerPrefs.GetFloat("ObjectScore", 0f);
            _textPuntuacionFinal.text = _finalScore.ToString("0");
            _textPuntuacionMoneda.text = coinScore.ToString("0");
            _textPuntuacionEnemigo.text = enemyScore.ToString("0");
            _textPuntuacionObjecto.text = objectScore.ToString("0");

            _MaxScore=PlayerPrefs.GetFloat("MaxScore",0f);

            WritePlayerRanking();
            WriteTimingNumber();
        }
    }
}
