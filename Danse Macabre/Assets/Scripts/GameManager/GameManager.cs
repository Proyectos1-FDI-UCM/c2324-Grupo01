using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager _UIManager;
    [SerializeField]
    private ScoreManager _ScoreManager;
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
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

    private void Start()
    {
        _UIManager = GetComponent<UIManager>();
        if (_UIManager == null) Debug.LogError("UIManager missing in GameManager!");

        _ScoreManager = GetComponent<ScoreManager>();
        if (_UIManager == null) Debug.LogError("ScoreManager in GameManager!");
    }

    #region methods
    public void ArrowTiming(string timing)
    {
        _UIManager.DisplayTiming(timing);
        _ScoreManager.AddTimingPoints(timing);
    }

    #endregion
}
