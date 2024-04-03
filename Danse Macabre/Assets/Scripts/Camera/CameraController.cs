using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _cameraTransform;
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private float horizontalCameraOffset = 7f; // Distancia horizontal constante entre jugador y centro de la c�mara
    [SerializeField]
    private float verticalCameraOffset = 2.445f; // Distancia vertical ideal (para el seguimiento vertical) entre jugador y centro de la c�mara
    [SerializeField]
    private float cameraHeight = 10; // Altura total de la c�mara
    [SerializeField]
    private float defaultLerpSpeed = 1f; // Velocidad a la que la c�mara se ajusta verticalmente (0-1) por defecto
    [SerializeField]
    private float topMargin = 3; // Distancia desde el borde superior de la c�mara a la que comenzar a subirla durante el seguimiento vertical

    [SerializeField]
    private bool allowFollow = true; // Permite el seguimiento del jugador en general. Desactivar y activar con ChangeFollow()
    [SerializeField]
    private bool allowVerticalFollow = true; // Permite el seguimiento vertical del jugador. Desactivar y activar con ChangeVerticalFollow()

    private bool goingUp = false; // Variable de control del seguimiento vertical
    private bool goingDown = false; // Variable de control del seguimiento vertical
    private bool end = false; // Indica que se ha llegado al final del nivel

    private float endTargetPosition; // Variable de control del lerp del final del nivel
    private float currentLerpTime = 0f; // Variable de control del lerp del final del nivel
    [SerializeField]
    private float finalLerpDuration = 2f; // Duraci�n del lerp final del nivel
    [SerializeField]
    private float finalLerpDistance = 5f; // Distancia que recorre el lerp final del nivel

    void Start()
    {
        _cameraTransform = transform;
        cameraHeight = cameraHeight / 2; // Para convertirlo en distancia desde el centro hasta el borde superior o inferior

        // To match initial offset of the character with the cameras
        // allowVerticalFollow = true;
        // TrackPlayer(_playerTransform.position.y - _cameraTransform.position.y);
        // FollowPlayer();
        // allowVerticalFollow = false;
    }

    void LateUpdate()
    {
        if (end)
        {
            currentLerpTime += Time.deltaTime; // Incrementar temporizador de lerp
            if (currentLerpTime > finalLerpDuration) // Prevenir exceso por encima de la duraci�n del lerp final
            {
                currentLerpTime = finalLerpDuration;
            }

            // C�lculo del Lerp
            float lerpPercent = currentLerpTime / finalLerpDuration;
            // lerpPercent = Mathf.Sin(lerpPercent * Mathf.PI * 0.5f); // Curva de easing #1: EaseOut
            lerpPercent = lerpPercent * lerpPercent * ((3 * lerpPercent) - (2f * lerpPercent)); // Curva de easing #2: Smoothstep

            // Lerp
            _cameraTransform.position = new Vector3(Mathf.Lerp(_cameraTransform.position.x, endTargetPosition, lerpPercent), _cameraTransform.position.y, -20);
        }
        else if (allowFollow)
        {
            TrackPlayer(_playerTransform.position.y - _cameraTransform.position.y);
            FollowPlayer();
        }
    }

    private void TrackPlayer(double distance)
    {
        //Debug.Log(distance + " " + cameraHeight + " " + -(cameraHeight));

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
            //Debug.Log("Bajando");
            targetY = _playerTransform.position.y + verticalCameraOffset;
        }
        else if (!goingDown && goingUp && allowVerticalFollow)  // Subiendo
        {
            //Debug.Log("Subiendo");
            targetY = Mathf.Lerp(_cameraTransform.position.y, Mathf.Round(_playerTransform.position.y + verticalCameraOffset), defaultLerpSpeed * Time.deltaTime);
        }
        else // En medio quieta
        {
            //Debug.Log("En medio");
            targetY = _cameraTransform.position.y;
        }

        _cameraTransform.position = new Vector3(_playerTransform.position.x + horizontalCameraOffset, targetY, -20);
    }

    public void ChangeFollow()
    {
        allowFollow = !allowFollow;
        //Debug.Log("Seguimiento general ahora es " + allowFollow);
    }

    public void ChangeVerticalFollow()
    {
        allowVerticalFollow = !allowVerticalFollow;
        //Debug.Log("Seguimiento vertical ahora es " + allowVerticalFollow);
    }
    public void EndOfLevelCamera()
    {
        allowFollow = false;
        end = true;
        endTargetPosition = Mathf.Round(_cameraTransform.position.x + finalLerpDistance);
        Debug.Log("FINAL DE NIVEL; parando c�mara en X = " + endTargetPosition);
    }
}