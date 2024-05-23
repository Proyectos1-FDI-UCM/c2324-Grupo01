using UnityEngine;

public class MusicColliderComponent : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject musicManager;
    private MusicManager musicManagerComponent;
    private ScoreManager _scoreManager;
    private ScreenBeatComponent _screenBeatComponent;
    #endregion

    void Start()
    {
        musicManagerComponent = musicManager.GetComponent<MusicManager>();
        _scoreManager = FindAnyObjectByType<ScoreManager>();
        _screenBeatComponent = FindAnyObjectByType<ScreenBeatComponent>();
    }

    /// <summary>
    /// When the player collides the object with this component:
    /// - Music starts playing
    /// - Score manager starts adding time points
    /// - Screen beat starts
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionComponent player = collision.GetComponent<ActionComponent>();

        if (player)
        {
            musicManagerComponent.PlayMusic();
            _scoreManager.GameStart();
            _screenBeatComponent.StartBeat();
        }
    }
}
