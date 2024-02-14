using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController:MonoBehaviour
{
    private Transform _cameraTransform;
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private float cameraOffset = 20f;
    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _cameraTransform.position = new Vector2(_playerTransform.position.x + cameraOffset, _cameraTransform.position.y);
    }
}
