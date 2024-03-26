using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private UIManager _UIManager;
    private ScoreManager _ScoreManager;
    [SerializeField]
    private GameObject MusicManager;
    private AudioSource _audioSource;
    private MusicManager _musicManager;
    [SerializeField]
    private GameObject Player;
    private MovementComponent _playerMovement;
    private Transform _playerTransform;
    [SerializeField]
    private GameObject StartCollider;
    private Transform _startColliderTransform;

    
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    Vector3 checkpointPosition;


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

        //DontDestroyOnLoad(this);
    }

    private void Start() // with defensive programming
    {
        _UIManager = GetComponent<UIManager>();
        if (_UIManager == null) Debug.LogError("UIManager missing in GameManager!");

        _ScoreManager = GetComponent<ScoreManager>();
        if (_UIManager == null) Debug.LogError("ScoreManager missing in GameManager!");

        _audioSource = MusicManager.GetComponent<AudioSource>();
        if (_audioSource == null) Debug.LogError("MusicManager's AudioSource missing in GameManager!");

        _musicManager = MusicManager.GetComponent<MusicManager>();
        if (_musicManager == null) Debug.LogError("MusicManager's MusicManager missing in GameManager!");

        _startColliderTransform = StartCollider.GetComponent<Transform>();
        if (_startColliderTransform == null) Debug.LogError("StartCollider's Transform missing in GameManager!");

        _playerMovement = Player.GetComponent<MovementComponent>();
        if (_playerMovement == null) Debug.LogError("Player's MovementComponent missing in GameManager!");

        _playerTransform = Player.GetComponent<Transform>();
        if (_playerTransform == null) Debug.LogError("Player's Transform missing in GameManager!");

    }

    #region methods
    public void PlayerHasDied()
    {
        if (checkpointPosition != new Vector3(0, 0, 0)){ // if a checkpoint exists
            LoadCheckpoint();
        }
        else{
            _ScoreManager.SaveFinalScore();
            LoadDeathScene();
        }

    }

    private void LoadDeathScene(){
        //Guardar el nombre de la escena anterior para el bot�n restart
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        //Cambiar escena de muerte
        SceneManager.LoadScene(4);
    }

    public void CheckpointReached(Vector3 position)
    { // called by checkpoints
        checkpointPosition = position;
        _ScoreManager.SaveCheckpointScore();
    }

    public void LoadCheckpoint()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        _ScoreManager.LoadCheckpointScore();
        _playerTransform.position = checkpointPosition;

        float playerSpeed = _playerMovement.speed;
        float playerPosX = _playerTransform.position.x;
        float startColliderPosX = _startColliderTransform.position.x;

        float time = (playerPosX - startColliderPosX)/playerSpeed;

        _audioSource.time = time;
        _musicManager.PlayMusic();
    }


    public void ArrowTiming(string timing)
    {
        _UIManager.DisplayTiming(timing);
        _ScoreManager.AddTimingPoints(timing);
    }

    #endregion
}
