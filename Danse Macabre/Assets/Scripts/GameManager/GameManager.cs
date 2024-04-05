using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    #region references
    private UIManager _UIManager;
    private ScoreManager _ScoreManager;
    [SerializeField]
    private GameObject Player;
    private MovementComponent _playerMovement;
    private Transform _playerTransform;
    private Rigidbody2D _playerRB;
    [SerializeField]
    private GameObject StartCollider;
    private Transform _startColliderTransform;
    private LevelDataLoader _levelDataLoader;
    #endregion
  
    #region properties
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    private static Vector3 checkpointPosition;
    private bool hasCheckpoint = false; 
    private static float playerSpeed;
    public float PlayerSpeed
    {
        get { return playerSpeed; }
        set { playerSpeed = value; }
    }

    private string previousScene = "";
    #endregion

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
        LoadAllReferences();
    }

    #region methods
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    { // Runs whenever a scene is loaded
        GameManager.Instance.LoadAllReferences();
        GameManager.Instance.LoadLevelData();
        MusicManager.Instance.LoadAllReferences();

        print("scene: " + SceneManager.GetActiveScene().name );

        if (previousScene != SceneManager.GetActiveScene().name)
        {
            hasCheckpoint = false;
            previousScene = SceneManager.GetActiveScene().name;
        }
        else hasCheckpoint = true;

        if (hasCheckpoint)
        {
            LoadCheckpoint();
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void PlayerHasDied()
    {
        if (hasCheckpoint)
        { // if a checkpoint exists
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        { // if there's no checkpoint
            _ScoreManager.SaveFinalScore();
            LoadDeathScene();
        }
    }
    private void LoadDeathScene()
    {
        //Guardar el nombre de la escena anterior para el bot�n restart
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        //Cambiar escena de muerte
        SceneManager.LoadScene(4);
    }


    public void CheckpointReached(Vector3 position)
    {
        hasCheckpoint = true;
        checkpointPosition = position;
        _ScoreManager.SaveCheckpointScore();
    }
    private void LoadCheckpoint()
    {
        _ScoreManager.LoadCheckpointScore();

        _playerMovement.InitialPosition(checkpointPosition);

        float startColliderPosX = _startColliderTransform.position.x;
        float time = math.abs(checkpointPosition.x - startColliderPosX)/PlayerSpeed;

        MusicManager.Instance.ChangeTime(time);
        MusicManager.Instance.PlayMusic();
    }

    public void LoadLevelData()
    {
        PlayerSpeed = _levelDataLoader.GetCurrentScenePlayerSpeed();
    }

    private void LoadAllReferences()
    {
        _UIManager = GetComponent<UIManager>();
        if (_UIManager == null) Debug.LogError("UIManager missing in GameManager!");

        _ScoreManager = GetComponent<ScoreManager>();
        if (_ScoreManager == null) Debug.LogError("ScoreManager missing in GameManager!");

        _startColliderTransform = StartCollider.GetComponent<Transform>();
        if (_startColliderTransform == null) Debug.LogError("StartCollider's Transform missing in GameManager!");

        _playerMovement = Player.GetComponent<MovementComponent>();
        if (_playerMovement == null) Debug.LogError("Player's MovementComponent missing in GameManager!");

        _playerRB = Player.GetComponent<Rigidbody2D>();
        if (_playerRB == null) Debug.LogError("Player's RigidBody missing in GameManager!");

        _playerTransform = Player.GetComponent<Transform>();
        if (_playerTransform == null) Debug.LogError("Player's Transform missing in GameManager!");

        _levelDataLoader = GetComponent<LevelDataLoader>();
        if (_levelDataLoader == null) Debug.LogError("Level data missing in GameManager!");
    }

    public void ArrowTiming(string timing)
    {
        _UIManager.DisplayTiming(timing);
        _ScoreManager.AddTimingPoints(timing);
    }
    #endregion
}
