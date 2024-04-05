using UnityEngine;

public class CameraChangerComponent : MonoBehaviour
{
    [SerializeField]
    CameraController _cameraController;

    [SerializeField]
    bool ChangeFollow = false; // Para activar/desactivar el seguimiento
    [SerializeField]
    bool ChangeVerticalFollow = false; // Para activar/desactivar el seguimiento vertical
    [SerializeField]
    bool EndOfLevel = false; // Si es para parar la c�mara al final del nivel

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
                _cameraController.ChangeVerticalFollow();
            }
            if (EndOfLevel)
            {
                _cameraController.EndOfLevelCamera();
            }

            _cameraController.SaveCurrentFollowState(); // For checkpoints
        }
    }
}
