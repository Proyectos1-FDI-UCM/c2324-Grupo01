using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _cameraTransform;
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private float cameraOffset = 7f; // Distancia horizontal entre jugador y centro de la cámara
    [SerializeField]
    private float targetVerticalOffset = 2.445f; // Distancia vertical entre jugador y centro de la cámara
    [SerializeField]
    private float cameraHeight = 10; // Altura total de la cámara
    [SerializeField]
    private float defaultLerpSpeed = 0.5f; // Velocidad a la que la cámara se ajusta verticalmente (0-1) por defecto

    private bool goingUp = false;
    private bool goingDown = true;

    void Start()
    {
        _cameraTransform = transform;
        cameraHeight = cameraHeight / 2;
    }

    void LateUpdate()
    {
        double distance = _playerTransform.position.y - _cameraTransform.position.y;TrackPlayer(distance);
        TrackPlayer(distance);
        FollowPlayer();
    }

    private void TrackPlayer(double distance)
    {
        Debug.Log(distance + " " + cameraHeight + " " + -(cameraHeight));

        if (distance < -targetVerticalOffset - 0.05)
        {
            goingDown = true;
            goingUp = false;
            // bajando
        }
        else if (distance > cameraHeight - 2)
        {
            goingDown = false;
            goingUp = true;
            // subiendo
        }
        else
        {
            goingDown = false;
            goingUp = false;
            // en medio
        }
    }

    private void FollowPlayer()
    {
        
        float targetY;

        if (goingDown && !goingUp) // Bajando
        {
            Debug.Log("Bajando");
            targetY = _playerTransform.position.y + targetVerticalOffset;
        }
        else if (!goingDown && goingUp)  // Subiendo
        {
            Debug.Log("Subiendo");
            targetY = Mathf.Lerp(_cameraTransform.position.y, Mathf.Round(_playerTransform.position.y + targetVerticalOffset), 1f * Time.deltaTime);
        }
        else // En medio quieta
        {
            Debug.Log("En medio");
            targetY = _cameraTransform.position.y;
        }
        
        _cameraTransform.position = new Vector3(_playerTransform.position.x + cameraOffset, targetY, -20f); //-20 porque la camara se movia al z = 0 si V2
    }
}
