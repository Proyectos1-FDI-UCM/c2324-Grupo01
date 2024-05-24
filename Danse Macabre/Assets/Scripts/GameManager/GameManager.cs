using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region paramenters
    #endregion

    #region references
    private CheckpointManager _checkpointManager;

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

    private LifeManager _lifeManager;

    [SerializeField]
    private GameObject StartCollider;
    private Transform _startColliderTransform;

    private LevelDataLoader _levelDataLoader;

    [SerializeField]
    private Canvas _DeathFilter;
    [SerializeField]
    private DeathFilterColor _DeathFilterColor;
    #endregion
  
    #region properties
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

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
        _DeathFilter.enabled = false;
    }

    #region methods
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // Runs whenever a scene is loaded
    { 
        GameManager.Instance.LoadLevelData();
        MusicManager.Instance.LoadAllReferences();

        if (SceneHasChanged())
        {
            _checkpointManager.ResetCheckpoint();
            _lifeManager.ResetPlayerLife(); // INTENTOS
            //_lifeManager.ResetTries();
            previousScene = SceneManager.GetActiveScene().name;
        }

        if (_checkpointManager.Checkpoint)
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

    private void PlayerHasDied()
    {
        if (!_lifeManager.PlayerHasLife()) // no life remaining
        {
            _checkpointManager.ResetCheckpoint();
            _ScoreManager.SaveFinalScore();
            MaxScoreCalculator.Instance.SaveSceneMaxScore();
            _sliderController.SaveProgess();
            LoadDeathScene();
        }
        else if (_checkpointManager.Checkpoint)  // INTENTOS
        { // if a checkpoint exists and has life
            SceneManager.LoadScene(previousScene);
        }
    }

    public IEnumerator PlayerDeath()
    {
        _DeathFilter.enabled = true;
        _DeathFilterColor.ColorChange();
        MusicManager.Instance.StopPlayingSong();
        _ScoreManager.GameStart(false);
        _lifeManager.PlayerLosesLife(); // INTENTOS
        //IncrementTries();

        float endTime = Time.time + 1.2f;

        while (Time.time < endTime)
        {
            yield return null;
        }

        PlayerHasDied();
    }

    private void LoadDeathScene()
    {
        //Guardar el nombre de la escena anterior para el bot�n restart
        //PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("PreviousScene", SceneManager.GetActiveScene().buildIndex); // stores scene as it's int index in the build
        //Cambiar escena de muerte
        SceneManager.LoadScene("Muerte");
    }

    private IEnumerator LoadCheckpoint()
    {
        float startTime = Time.time;
        float durationTime = _RespawnCountDown.RespawnTime;
        float endTime = startTime + durationTime;

        _ScoreManager.LoadCheckpointScore();
        _playerMovement.InitialPosition(_checkpointManager.CheckpointPosition);
        _cameraController.ResetToFramePlayer();
        _cameraController.SetFollowState();
        float startColliderPosX = _startColliderTransform.position.x;

        while (Time.time < endTime)
        {
            yield return null;
        }

        float time = math.abs(_checkpointManager.CheckpointPosition.x - startColliderPosX)/PlayerSpeed;
        _playerMovement.Autoscroll();
        MusicManager.Instance.ChangeTime(time);
        MusicManager.Instance.PlayMusic();
        _ScoreManager.GameStart(true);
    }


    public void LoadLevelData()
    {
        PlayerSpeed = _levelDataLoader.GetCurrentScenePlayerSpeed();
    }

    private void LoadAllReferences()
    {
        _checkpointManager = GetComponent<CheckpointManager>();

        _cameraController = _camera.GetComponent<CameraController>();

        _ScoreManager = GetComponent<ScoreManager>();

        _startColliderTransform = StartCollider.GetComponent<Transform>();

        _playerMovement = Player.GetComponent<MovementComponent>();

        _lifeManager = Player.GetComponent<LifeManager>();

        _levelDataLoader = GetComponent<LevelDataLoader>();

        _RespawnCountDown = FindObjectOfType<RespawnCountDown>();

        _sliderController = FindObjectOfType<SliderController>();
    }
    #endregion
}
