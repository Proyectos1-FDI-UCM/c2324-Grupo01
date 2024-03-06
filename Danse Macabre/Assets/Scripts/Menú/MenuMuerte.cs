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
    void Start()
    {

    }
}
