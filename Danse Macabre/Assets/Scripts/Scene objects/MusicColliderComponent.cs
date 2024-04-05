using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicColliderComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject musicManager;
    private MusicManager musicManagerComponent;
    private ScoreManager _scoreManager;
    void Start()
    {
        musicManagerComponent = musicManager.GetComponent<MusicManager>();
        _scoreManager=FindAnyObjectByType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionComponent player = collision.GetComponent<ActionComponent>();

        if (player)
        {
            musicManagerComponent.PlayMusic();
            _scoreManager.GameStart();
        }
    }
}
