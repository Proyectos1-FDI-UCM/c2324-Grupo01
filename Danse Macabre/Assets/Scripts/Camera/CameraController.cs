using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController:MonoBehaviour
{
    private Transform _cameraTransform;
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private float cameraOffset = 7f;
    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = transform;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _cameraTransform.position = new Vector3(_playerTransform.position.x + cameraOffset, _cameraTransform.position.y, -20f); //-20 porque la camara se movia al z = 0 si V2
    }
}
