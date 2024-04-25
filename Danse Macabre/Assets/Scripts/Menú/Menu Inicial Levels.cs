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
        Time.timeScale = 1f;
        SceneManager.LoadScene("TutorialV3");
    }
    public void Jugar_Nivel1() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Nivel GnorkParty");
    }
    public void Jugar_Nivel2() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Nivel_Dear_X");
    }
    public void Jugar_Nivel3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Nivel_Sugar");
    }
    public void Jugar_Prueba()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("NivelPruebaDash");
    }

    public void Salir()
    {
        PlayerPrefs.SetInt("MenuLevelActivo", 0);
        Application.Quit();
    }
    #endregion
    void Start()
    {
        if (PlayerPrefs.HasKey("MenuLevelActivo"))
        {
            int isActiveMenuLevel = PlayerPrefs.GetInt("MenuLevelActivo");
            if (isActiveMenuLevel == 1) 
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
