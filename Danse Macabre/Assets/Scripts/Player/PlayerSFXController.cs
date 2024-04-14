using UnityEngine;

public class PlayerSFXController : MonoBehaviour
{
    #region references
    private ActionComponent _myAction;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip[] audioClips = new AudioClip[4]; 
    #endregion
    void Start()
    {
        _myAction = GetComponent<ActionComponent>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( _myAction != null )
        {
            
            if (_myAction.currentAction == ActionComponent.Action.Jumping)
            {
                Debug.Log("Entrado");
                _audioSource.PlayOneShot(audioClips[0]);
            }
            else if (_myAction.currentAction == ActionComponent.Action.Stomping)
            {
                _audioSource.PlayOneShot(audioClips[1]);
                Debug.Log("Entrando 2");
            }
        }
    }
}
