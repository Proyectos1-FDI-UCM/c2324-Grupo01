using UnityEngine;

public class MissedComponent : MonoBehaviour
{
    private ArrowComponent _myArrow;

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
                GameManager.Instance.ArrowTiming("MISSED");
                _myArrow.GetComponent<ArrowComponent>().DeactivateGray();
            }
        }
    }
    void Start()
    {
        _myArrow = GetComponentInParent<ArrowComponent>();
    }

}
