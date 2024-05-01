using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{
    public void Quit()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("MenuLevelActivo", 0);
        SceneManager.LoadScene("Menu Inicial Level");
    }
}
