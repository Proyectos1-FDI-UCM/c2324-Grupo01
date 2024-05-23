using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    #region references
    private ActionComponent _actionComponent;
    private Animator _myAnimator;
    [SerializeField] private TempoManager _Tempo;
    #endregion

    #region properties
    private float AnimSpeed;
    #endregion

    void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _actionComponent = GetComponent<ActionComponent>();

        // Calculates and sets animation speed based on Tempo.
        AnimSpeed = _Tempo.SecondsPerTick * 3;
        _myAnimator.SetFloat("RunSpeed", AnimSpeed);
    }
    void Update()
    {
        if (_actionComponent.currentAction == ActionComponent.Action.Running) _myAnimator.SetInteger("State", 0);
        else if (_actionComponent.currentAction == ActionComponent.Action.Jumping) _myAnimator.SetInteger("State", 1);
        else if (_actionComponent.currentAction == ActionComponent.Action.Stomping) _myAnimator.SetInteger("State", 2);
        else if (_actionComponent.currentAction == ActionComponent.Action.Sliding) _myAnimator.SetInteger("State", 3);
        else if (_actionComponent.currentAction == ActionComponent.Action.Dashing 
                || _actionComponent.currentAction == ActionComponent.Action.Falling) _myAnimator.SetInteger("State", 5);
    }

    /// <summary>
    /// Deactivate animation when the player dies.
    /// </summary>
    public void ToggleAnimationOff()
    {
        _myAnimator.enabled = false;
    }
}
