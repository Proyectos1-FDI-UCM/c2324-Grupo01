using System.Runtime.InteropServices;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{
    private Transform _myTransform;
    [SerializeField] private GameObject _manager;
    private CheckpointManager _checkpointManager;

    void Start()
    {
        _myTransform = transform;
        _checkpointManager = _manager.GetComponent<CheckpointManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MovementComponent player = other.GetComponent<MovementComponent>();

        if (player != null)
        {
            _checkpointManager.CheckpointReached(player.transform.position);
        }
    }
}
