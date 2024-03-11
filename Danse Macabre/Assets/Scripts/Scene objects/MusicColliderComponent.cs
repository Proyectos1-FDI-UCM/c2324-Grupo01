using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicColliderComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject musicManager;
    private MusicManager musicManagerComponent;

    void Start()
    {
        musicManagerComponent = musicManager.GetComponent<MusicManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionComponent player = collision.GetComponent<ActionComponent>();

        if (player)
        {
            //print("time: " + Time.time);
            musicManagerComponent.PlayMusic();
        }
    }
}
