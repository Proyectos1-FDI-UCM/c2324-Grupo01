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
    private float horizontalCameraOffset = 7f; // Distancia horizontal constante entre jugador y centro de la cámara
    [SerializeField]
    private float verticalCameraOffset = 2.445f; // Distancia vertical ideal (para el seguimiento vertical) entre jugador y centro de la cámara
    [SerializeField]
    private float cameraHeight = 10; // Altura total de la cámara
    [SerializeField]
    private float defaultLerpSpeed = 1f; // Velocidad a la que la cámara se ajusta verticalmente (0-1) por defecto
    [SerializeField]
    private float topMargin = 3; // Distancia desde el borde superior de la cámara a la que comenzar a subirla durante el seguimiento vertical

    [SerializeField]
    private bool allowFollow = true; // Permite el seguimiento del jugador en general. Desactivar y activar con ChangeFollow()
    [SerializeField]
    private bool allowVerticalFollow = true; // Permite el seguimiento vertical del jugador. Desactivar y activar con ChangeVerticalFollow()

    private bool goingUp = false;
    private bool goingDown = true;

    void Start()
    {
        _cameraTransform = transform;
        cameraHeight = cameraHeight / 2; // Para convertirlo en distancia desde el centro hasta el borde superior o inferior
    }

    void LateUpdate()
    {
        if (allowFollow)
        {
            double distance = _playerTransform.position.y - _cameraTransform.position.y; TrackPlayer(distance);
            TrackPlayer(distance);
            FollowPlayer();
        }
    }

    private void TrackPlayer(double distance)
    {
        Debug.Log(distance + " " + cameraHeight + " " + -(cameraHeight));

        if (distance < -verticalCameraOffset - 0.05)
        {
            goingDown = true;
            goingUp = false;
            // bajando
        }
        else if (distance > cameraHeight - topMargin)
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

        if (goingDown && !goingUp && allowVerticalFollow) // Bajando
        {
            Debug.Log("Bajando");
            targetY = _playerTransform.position.y + verticalCameraOffset;
        }
        else if (!goingDown && goingUp && allowVerticalFollow)  // Subiendo
        {
            Debug.Log("Subiendo");
            targetY = Mathf.Lerp(_cameraTransform.position.y, Mathf.Round(_playerTransform.position.y + verticalCameraOffset), defaultLerpSpeed * Time.deltaTime);
        }
        else // En medio quieta
        {
            Debug.Log("En medio");
            targetY = _cameraTransform.position.y;
        }

        _cameraTransform.position = new Vector3(_playerTransform.position.x + horizontalCameraOffset, targetY, -20);
    }
}
