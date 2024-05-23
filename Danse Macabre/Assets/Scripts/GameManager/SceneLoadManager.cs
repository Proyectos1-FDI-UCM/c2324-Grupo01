using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    /// <summary>
    /// Load procedure for 
    /// </summary>
    public void CheckpointLoadRequirements()
    {
        MinimunLevelLoadRequirements();
    }

    /// <summary>
    /// All that needs to be loaded for any level: when playing for the first time and starting from checkpoint.
    /// </summary>
    public void MinimunLevelLoadRequirements()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // Runs whenever a scene is loaded
    // { 
    //     GameManager.Instance.DebugGM();
    // }
    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }
    // void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }
}
