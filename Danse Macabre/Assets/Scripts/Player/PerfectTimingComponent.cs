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
    private LayerMask interactiveObjectLayer;
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
    public void CheckNearbyArrow()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(_myTransform.position, badRadius, interactiveObjectLayer);

        if (hitCollider == null) {
            GameManager.Instance.ArrowTiming("MISSED");
            print("MISSED!");
        }
        else {
            if (_playerAction._isJumping) targetTag = "Jump";
            else if (_playerAction.isStomping) targetTag = "Stomp";
            else if (_playerAction.isDashing || _playerAction.isSliding) targetTag = "DashSlide";

            float distance = Vector2.Distance(_myTransform.position, hitCollider.transform.position);

            if (hitCollider.CompareTag(targetTag)) {

                // mejor que sea en un UI o GM pero...
                hitCollider.gameObject.GetComponent<ArrowComponent>().Deactivate();
            

                if (distance <= perfectRadius)
                {
                    // Calls game manager, that calls UI and ScoreI
                    GameManager.Instance.ArrowTiming("PERFECT");
                    print("Perfect!!!");
                }
                else if (distance <= goodRadius)
                {
                    GameManager.Instance.ArrowTiming("GOOD");
                    print("Good!");
                }
                else
                {
                    GameManager.Instance.ArrowTiming("BAD");
                    print("Bad...");
                }
            }
            else {
                GameManager.Instance.ArrowTiming("WRONG");
                print("WRONG MOVE!!!");
            }
        }
    }
    #endregion

}