using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButtonEnabler : MonoBehaviour
{
    #region references
    [SerializeField]
    private TextMeshProUGUI _text;
    #endregion

    void Start()
    {
        // If it's the last scene of the build the next button won't appear.
        if (PlayerPrefs.GetInt("PreviousScene") == SceneManager.sceneCountInBuildSettings - 2)
        {
            _text.text = "Credits";
        }
        else _text.text = "Next Level";
    }
}
