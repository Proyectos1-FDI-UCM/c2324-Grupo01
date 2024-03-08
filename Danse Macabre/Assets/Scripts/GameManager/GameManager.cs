using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private ScoreManager _scoreManager;
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public void Muerte(){
        _scoreManager.SaveFinalScore();
        //Cambiar escena de muerte
        SceneManager.LoadScene(4);
    }

    // public void Muerte()
    // {
    //     Time.timeScale = 0f;
    //     MusicManager.Instance.StopPlayingSong();
    //     menuMuerte.SetActive(true);
    // }
   private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Update()
    {
        
    }
}
