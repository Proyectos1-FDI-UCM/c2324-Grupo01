// ASSIGN TO THE ARROW'S OBJECT
using UnityEngine;

public class ArrowComponent : MonoBehaviour
{
    #region paramaters
    //Parameters used to drawn Gizmos:
    private float perfectRadius = 0.2f;
    private float goodRadius = 0.35f;
    private float badRadius = 0.5f;
    #endregion

    #region refrences
    private Animator _myAnimator;
    private SpriteRenderer _myRenderer;
    [SerializeField]
    private Sprite graySprite;
    [SerializeField]
    private float DesaturationMultiplier = 1.5f; // Determines how much colour is drained of an arrow on a succesful action (turns gray on miss)
    #endregion

    #region properties
    [SerializeField]
    private bool activateGizmos = false;
    private bool actionDone = false; // Bool that tracks if an any action was made inside the arrow collider.
    #endregion

    private void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _myRenderer = GetComponent<SpriteRenderer>();
        MaxScoreCalculator.Instance.ArrowRegister();
    }

    #region methods
    /// <summary>
    /// Called when an input of an action is done inside the arrow collider.
    /// </summary>
    public void ActionDone()
    {
        actionDone = true;
        DeactivateDark();
    }
    /// <summary>
    /// Checks if an action was already done inside this arrow.
    /// Missed Component from the edge collider object calls it to give it a missed if case.
    /// </summary>
    /// <returns>If an action was made, TRUE. If not, FALSE.</returns>
    public bool IsDone()
    {
        return actionDone;
    }

    /// <summary>
    /// When an action is MISSED this method turns the arrow to gray. This is called from MissedComponent only.
    /// </summary>
    public void DeactivateGray()
    {
        if (!actionDone)
        {
            _myRenderer.sprite = graySprite;
        }
        _myAnimator.enabled = false;
    }

    /// <summary>
    /// When an action is DONE this method turns the arrow darker, but not gray. This is called from this script.
    /// </summary>
    private void DeactivateDark()
    {
        _myAnimator.enabled = false;
        _myRenderer.color = new Color(_myRenderer.color.r 
            / DesaturationMultiplier, _myRenderer.color.g 
            / DesaturationMultiplier, _myRenderer.color.b 
            / DesaturationMultiplier, _myRenderer.color.a);
    }
    #endregion

    /// <summary>
    /// Draws timing regions for design purposes.
    /// </summary>
    void OnDrawGizmos()
    {
        if (activateGizmos)
        {            
            CircleCollider2D collider = GetComponent<CircleCollider2D>();
            if (collider != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, perfectRadius);

                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, goodRadius);

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, badRadius);
            }
        }
    }
}