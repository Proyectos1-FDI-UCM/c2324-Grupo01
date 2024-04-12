using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{
    private Transform _myTransform;

    void Start()
    {
        _myTransform = transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MovementComponent player = other.GetComponent<MovementComponent>();

        if (player != null)
        {
            GameManager.Instance.CheckpointReached(player.transform.position);
        }
    }
}
