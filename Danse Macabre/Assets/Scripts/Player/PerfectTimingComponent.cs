// ASSIGN TO THE PLAYER'S OBJECT
using UnityEngine;

public class PerfectTimingComponent : MonoBehaviour
{
    #region parameters
    private float perfectRadius = 0.2f;
    private float goodRadius = 0.35f;
    private float badRadius = 0.5f;
    #endregion

    #region references
    private Transform _myTransform;
    [SerializeField]
    private LayerMask arrowLayer;
    private ActionComponent _playerAction;
    #endregion

    #region properties
    private string targetTag;
    #endregion

    private void Start()
    {
        _myTransform = transform;
        _playerAction = GetComponent<ActionComponent>();
    }

    #region methods
    /// <summary>
    /// Called everytime there's an action input (See InputManager).
    /// This method checks the distance between the player transform.position and the arrow's transform.position to calculate the timing of the action.
    /// </summary>
    public void CheckNearbyArrow()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(_myTransform.position, badRadius, arrowLayer); // "Draws" a circle around the player to check if any object in the arrowLayer is within range.
        
        if (hitCollider != null) // This is true if the player is within the bad radius range of an arrow.
        {
            ArrowComponent arrowComponent = hitCollider.gameObject.GetComponent<ArrowComponent>(); // Reference for the arrow script.
            
            if (!arrowComponent.IsDone()) // If no action is registered in this arrow.
            {
                arrowComponent.ActionDone(); // Set this arrow as done so there's no double input.

                // Maps the player's action to a tag
                if (_playerAction.isStomping) targetTag = "Stomp";   
                else if (_playerAction._isJumping) targetTag = "Jump";
                else if (_playerAction.isDashing || _playerAction.isSliding) targetTag = "DashSlide";

                float distance = Vector2.Distance(_myTransform.position, hitCollider.transform.position); // Calculate the distance between player and arrow.

                if (hitCollider.CompareTag(targetTag)) // Is true if the mapped tag (action) above is the same as the within range arrow.
                {
                
                    if (distance <= perfectRadius)
                    {
                        GameManager.Instance.ArrowTiming("PERFECT");
                    }
                    else if (distance <= goodRadius)
                    {
                        GameManager.Instance.ArrowTiming("GREAT");
                    }
                    else
                    {
                        GameManager.Instance.ArrowTiming("GOOD");
                    }
                }
                else // If the tag mapped (action) doesn't match the arrow's tag.
                {
                    GameManager.Instance.ArrowTiming("WRONG"); // if the movement is not correct
                }
            }
            
        }    
    }
    #endregion

}