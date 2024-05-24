using UnityEngine;

public class MissedComponent : MonoBehaviour
{
    #region parameters
    private readonly string missed = "MISSED";
    #endregion parameters

    #region references
    [SerializeField]
    private GameObject _player;
    private PerfectTimingComponent _timingComponent;
    private ArrowComponent _myArrow;
    #endregion references

    /// <summary>
    /// When the player passes by the vertical line of the arrow it checks if any action was made.
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        ActionComponent player = collision.gameObject.GetComponent<ActionComponent>();

        if (player != null)
        {
            if (!_myArrow.IsDone()) {
                _timingComponent.TimingHandler(missed);
                _myArrow.GetComponent<ArrowComponent>().DeactivateGray();
            }
        }
    }
    
    void Start()
    {
        _myArrow = GetComponentInParent<ArrowComponent>();
        _timingComponent = _player.GetComponent<PerfectTimingComponent>();
    }
}
