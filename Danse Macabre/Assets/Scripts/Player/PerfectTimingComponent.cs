using System.Collections;
using System.Collections.Generic;
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
    public void CheckNearbyArrow() // Called everytime there's an input
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(_myTransform.position, badRadius, arrowLayer);

        if (hitCollider != null)
        {
            hitCollider.gameObject.GetComponent<ArrowComponent>().ActionDone();

            if (_playerAction.isStomping) targetTag = "Stomp";   
            else if (_playerAction._isJumping) targetTag = "Jump";
            else if (_playerAction.isDashing || _playerAction.isSliding) targetTag = "DashSlide";

            float distance = Vector2.Distance(_myTransform.position, hitCollider.transform.position);

            if (hitCollider.CompareTag(targetTag)) {

                hitCollider.gameObject.GetComponent<ArrowComponent>().Deactivate(); // Deactivate arrow if the move is correct for that arrow
            
                if (distance <= perfectRadius)
                {
                    // Calls game manager, that calls UI and ScoreI
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
            else {
                GameManager.Instance.ArrowTiming("WRONG"); // if the movement is not correct
            }
        }    
    }
    #endregion

}