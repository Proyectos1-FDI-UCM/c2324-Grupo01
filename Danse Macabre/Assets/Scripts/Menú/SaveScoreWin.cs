using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScoreWin : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _manager;
    private CheckpointManager _checkpointManager;
    private ScoreManager _scoreManager;
    private SliderController _sliderController;
    #endregion
    
    #region methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MovementComponent _player = collision.GetComponent<MovementComponent>();
        if (_player != null)
        {
            _scoreManager.SaveFinalScore();
            _sliderController.SaveProgess();
            MaxScoreCalculator.Instance.SaveSceneMaxScore();
            //Cambiar escena de Victoria
            _checkpointManager.ResetCheckpoint();
            PlayerPrefs.SetInt("PreviousScene", SceneManager.GetActiveScene().buildIndex); // For restarting the same level after victory
            SceneManager.LoadScene("Victoria");
        }
    }
    #endregion

    void Start()
    {
        _checkpointManager = _manager.GetComponent<CheckpointManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        _sliderController = FindObjectOfType<SliderController>();
    }
}
