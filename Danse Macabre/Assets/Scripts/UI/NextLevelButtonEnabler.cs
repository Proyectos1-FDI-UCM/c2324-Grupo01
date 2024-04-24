using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButtonEnabler : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject NextButton;
    #endregion

    void Start()
    {
        // If it's the last scene of the build the next button won't appear.
        if (PlayerPrefs.GetInt("PreviousScene") == SceneManager.sceneCountInBuildSettings -1)
        {
            NextButton.SetActive(false);
        }
    }
}
