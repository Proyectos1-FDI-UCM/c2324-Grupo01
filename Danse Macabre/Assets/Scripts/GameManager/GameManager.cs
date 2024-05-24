using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region paramenters
    #endregion

    #region references
    [SerializeField]
    private GameObject _camera;
    private CameraController _cameraController;
    private ScoreManager _ScoreManager;
    private SliderController _sliderController;
    [SerializeField]
    private RespawnCountDown _RespawnCountDown;
    [SerializeField]
    private GameObject Player;
    private MovementComponent _playerMovement;
    private Transform _playerTransform;
    private LifeManager _lifeManager;
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
    private static bool hasCheckpoint = false; 
    private static bool cameraFollow;
    private static bool cameraVerticalFollow;
    private static float playerSpeed;
    public float PlayerSpeed
    {
        get { return playerSpeed; }
        set { playerSpeed = value; }
    }

    private static string previousScene = "";
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
        
        LoadAllReferences();
    }

    #region methods
    public void DebugGM()
    {
        print("GMMMMMMMMMMMM");
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // Runs whenever a scene is loaded
    { 
        GameManager.Instance.LoadLevelData();
        MusicManager.Instance.LoadAllReferences();

        if (SceneHasChanged())
        {
            hasCheckpoint = false;
            previousScene = SceneManager.GetActiveScene().name;
        }

        if (hasCheckpoint)
        {
            StartCoroutine(LoadCheckpoint());
        }
        else // if starting a level for the first time
        {
            _lifeManager.ResetPlayerLife(); // INTENTOS
            //ResetTries();
            Invoke("StartAutoscroll", _RespawnCountDown.RespawnTime);
        }
    }
    public void StartAutoscroll()
    {
        _playerMovement.Autoscroll();
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private bool SceneHasChanged()
    {
        return previousScene != SceneManager.GetActiveScene().name;
    }


    // CAMERA
    public void SaveCameraState(bool p_cameraFollow, bool p_cameraVerticalFollow) // Save current state to be loaded after checkpoint
    {
        cameraFollow = p_cameraFollow;
        cameraVerticalFollow = p_cameraVerticalFollow;
    }
    public (bool, bool) LoadCameraState() // Load camera state into camera when returning to checkpoint
    {
        return (cameraFollow, cameraVerticalFollow);
    }

    public void PlayerHasDied()
    {
        _lifeManager.PlayerLosesLife(); // INTENTOS
        //IncrementTries();

        if (CheckpointExists() && _lifeManager.PlayerHasLife())  // INTENTOS
        { // if a checkpoint exists
            SceneManager.LoadScene(previousScene);
        }
        else
        { // if there's no checkpoint
            _ScoreManager.SaveFinalScore();
            MaxScoreCalculator.Instance.SaveSceneMaxScore();
            _sliderController.SaveProgess();
            _lifeManager.ResetPlayerLife(); // INTENTOS
            //_lifeManager.ResetTries();
            LoadDeathScene();
        }
    }

    private void LoadDeathScene()
    {
        //Guardar el nombre de la escena anterior para el bot�n restart
        //PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("PreviousScene", SceneManager.GetActiveScene().buildIndex); // stores scene as it's int index in the build
        //Cambiar escena de muerte
        SceneManager.LoadScene("Muerte");
    }


    // CHECKPOINT
    public void ResetCheckpoint()
    {
        //_lifeManager.ResetTries(); // TRIES
        hasCheckpoint = false;
    }
    public bool CheckpointExists()
    {
        return hasCheckpoint;
    }
    public void CheckpointReached(Vector3 position)
    {
        hasCheckpoint = true;
        checkpointPosition = position;
        _ScoreManager.SaveCheckpointScore();
        _cameraController.SaveCurrentFollowState();
    }
    private IEnumerator LoadCheckpoint()
    {
        float startTime = Time.time;
        float durationTime = _RespawnCountDown.RespawnTime;
        float endTime = startTime + durationTime;

        _ScoreManager.LoadCheckpointScore();
        _playerMovement.InitialPosition(checkpointPosition);
        _cameraController.ResetToFramePlayer();
        _cameraController.SetFollowState();
        float startColliderPosX = _startColliderTransform.position.x;

        while (Time.time < endTime)
        {
            yield return null;
        }

        float time = math.abs(checkpointPosition.x - startColliderPosX)/PlayerSpeed;
        _playerMovement.Autoscroll();
        MusicManager.Instance.ChangeTime(time);
        MusicManager.Instance.PlayMusic();
        _ScoreManager.GameStart();
    }


    public void LoadLevelData()
    {
        PlayerSpeed = _levelDataLoader.GetCurrentScenePlayerSpeed();
    }

    private void LoadAllReferences()
    {
        _cameraController = _camera.GetComponent<CameraController>();
        if (_cameraController == null) Debug.LogError("CAMERA missing in GameManager!");

        _ScoreManager = GetComponent<ScoreManager>();
        if (_ScoreManager == null) Debug.LogError("ScoreManager missing in GameManager!");

        _startColliderTransform = StartCollider.GetComponent<Transform>();
        if (_startColliderTransform == null) Debug.LogError("StartCollider's Transform missing in GameManager!");

        _playerMovement = Player.GetComponent<MovementComponent>();
        if (_playerMovement == null) Debug.LogError("Player's MovementComponent missing in GameManager!");

        _lifeManager = Player.GetComponent<LifeManager>();

        _playerRB = Player.GetComponent<Rigidbody2D>();
        if (_playerRB == null) Debug.LogError("Player's RigidBody missing in GameManager!");

        _playerTransform = Player.GetComponent<Transform>();
        if (_playerTransform == null) Debug.LogError("Player's Transform missing in GameManager!");

        _levelDataLoader = GetComponent<LevelDataLoader>();
        if (_levelDataLoader == null) Debug.LogError("Level data missing in GameManager!");

        _RespawnCountDown = FindObjectOfType<RespawnCountDown>();

        _sliderController = FindObjectOfType<SliderController>();

    }
    #endregion
}
