using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    #region references
    
    #endregion

    #region properties
    private static string _myScene;
    private static CheckpointManager instance;
    public static CheckpointManager Instance
    {
        get { return instance; }
    }
    #endregion
    
    #region methods
    // public void ResetCheckpoint()
    // {
    //     numberOfTries = 0;
    //     hasCheckpoint = false;
    // }
    // public bool CheckpointExists()
    // {
    //     return hasCheckpoint;
    // }
    // public void CheckpointReached(Vector3 position)
    // {
    //     hasCheckpoint = true;
    //     checkpointPosition = position;
    //     _ScoreManager.SaveCheckpointScore();
    //     _cameraController.SaveCurrentFollowState();
    // }
    // private IEnumerator LoadCheckpoint()
    // {
    //     float startTime = Time.time;
    //     float durationTime = 2f;
    //     float endTime = startTime + durationTime;

    //     _ScoreManager.LoadCheckpointScore();
    //     _playerMovement.InitialPosition(checkpointPosition);
    //     _cameraController.ResetToFramePlayer();
    //     _cameraController.SetFollowState();
    //     float startColliderPosX = _startColliderTransform.position.x;

    //     while (Time.time < endTime)
    //     {
    //         yield return null;
    //     }

    //     float time = math.abs(checkpointPosition.x - startColliderPosX)/PlayerSpeed;
    //     _playerMovement.Autoscroll();
    //     MusicManager.Instance.ChangeTime(time);
    //     MusicManager.Instance.PlayMusic();
    //     _ScoreManager.GameStart();
    // }

    public void SetActiveScene(string activeScene)
    {
        _myScene = activeScene;
    }
    #endregion

//    private void Awake()
//     {
//         if (Instance == null)
//         {
//             instance = this;

//         }
//         else
//         {
//             Destroy(instance);
//         }

//     }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
