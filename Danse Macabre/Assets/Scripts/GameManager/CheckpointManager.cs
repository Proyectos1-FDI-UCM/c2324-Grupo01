using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _camera;
    private CameraController _cameraController;
    private ScoreManager _scoreManager;
    #endregion

    #region properties
    private static bool checkpoint = false;
    public bool Checkpoint
    {
        get { return checkpoint; }
    }
    private static Vector3 checkpointPosition;
    public Vector3 CheckpointPosition
    {
        get { return checkpointPosition; }
    }
    #endregion properties
    
    #region methods
    public void ResetCheckpoint()
    {
        //_lifeManager.ResetTries(); // TRIES
        checkpoint = false;
    }

    public void CheckpointReached(Vector3 position)
    {
        checkpoint = true;
        checkpointPosition = position;
        _scoreManager.SaveCheckpointScore();
        _cameraController.SaveCurrentFollowState();
    }
    #endregion methods

    private void Start()
    {
        _scoreManager = GetComponent<ScoreManager>();
        _cameraController = _camera.GetComponent<CameraController>(); 
    }
}
