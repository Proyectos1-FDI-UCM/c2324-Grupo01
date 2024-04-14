 // ASSIGN TO THE ARROW'S OBJECT
 using UnityEngine;

public class ArrowComponent : MonoBehaviour
{
    #region paramaters
    // Parameters used to drawn Gizmos:
    // private float perfectRadius = 0.2f;
    // private float goodRadius = 0.35f;
    // private float badRadius = 0.5f;
    #endregion

    #region refrences
    private Animator _myAnimator;
    private SpriteRenderer _myRenderer;
    [SerializeField]
    private Sprite graySprite;
    #endregion

    #region properties
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
        Deactivate();
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
    /// When an action is done this method turns the arrow to gray.
    /// </summary>
    private void Deactivate()
    {
        _myAnimator.enabled = false;
        _myRenderer.sprite = graySprite;
    }
    #endregion

    // void OnDrawGizmos()
    // {
    //     CircleCollider2D collider = GetComponent<CircleCollider2D>();
    //     if (collider != null)
    //     {
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, perfectRadius);

    //         Gizmos.color = Color.blue;
    //         Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, goodRadius);

    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, badRadius);
    //     }
    // }
}