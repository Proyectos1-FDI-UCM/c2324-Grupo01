using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraChangerComponent : MonoBehaviour
{
    CameraController _cameraController;

    [SerializeField]
    bool ChangeFollow = false; // Para activar/desactivar el seguimiento
    [SerializeField]
    bool ChangeVerticalFollow = false; // Para activar/desactivar el seguimiento vertical
    [SerializeField]
    bool EndOfLevel = false; // Si es para parar la c�mara al final del nivel
    [SerializeField]
    float ApproximatePlayerHeight = 1.0f; // Tamaño aproximado del jugador en worldspace
    [SerializeField]
    float ApproximateCameraToFloorHeight = 5.0f; // Distancia aproximada del centro de la cámara al suelo

    float colliderHeight;

    private void Start()
    {
        float colliderHeight = this.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprueba si esta colisionando con el personaje
        ActionComponent _player = collision.gameObject.GetComponent<ActionComponent>();
        if (_player)
        {
            if (ChangeFollow)
            {
                _cameraController.ChangeFollow();
            }
            if (ChangeVerticalFollow)
            {
                _cameraController.SetVerticalHeight(this.transform.position.y - (colliderHeight / 2) + (ApproximatePlayerHeight / 2) + _cameraController.verticalCameraOffset - ApproximateCameraToFloorHeight);
                _cameraController.ChangeVerticalFollow();
            }
            if (EndOfLevel)
            {
                _cameraController.EndOfLevelCamera();
            }
        }
    }
}
