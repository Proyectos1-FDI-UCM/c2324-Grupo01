using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialLevels : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject menuLevel;
    [SerializeField] private GameObject menuInicio;

    private MenuFinalJuego menuFinal;
    #endregion
    #region methods
    public void OnPlayBottonClicked()   
    {
        menuLevel.SetActive(true);
        menuInicio.SetActive(false);
    }
    public void Jugar_Tutorial() 
    {
        SceneManager.LoadScene("TutorialV3");
        Time.timeScale = 1.0f;
    }
    public void Jugar_Nivel1() 
    {
        SceneManager.LoadScene("Nivel GnorkParty");
        Time.timeScale = 1.0f;
    }
    public void Jugar_Nivel2() 
    {
        SceneManager.LoadScene("Nivel_Dear_X");
        Time.timeScale = 1.0f;
    }
    public void Jugar_Nivel3()
    {
        SceneManager.LoadScene("Nivel_Sugar");
        Time.timeScale = 1.0f;
    }
    public void Jugar_Prueba()
    {
        SceneManager.LoadScene("NivelPruebaDash");
        Time.timeScale = 1.0f;
    }

    public void Credits()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void Story()
    {
        SceneManager.LoadScene("Historia");
    }

    public void Salir()
    {
        PlayerPrefs.SetInt("MenuLevelActivo", 0);
        Application.Quit();
    }
    #endregion
    void Start()
    {
        if (PlayerPrefs.HasKey("MenuLevelActivo")) //si desde fin de juego se ha guardado el int
        {
            if (PlayerPrefs.GetInt("MenuLevelActivo") == 1) 
            {
                menuLevel.SetActive(true);
                menuInicio.SetActive(false);
            }
            else
            {
                 menuLevel.SetActive(false);
                menuInicio.SetActive(true);
            }
        }
    }
}
