using UnityEngine;

public class MovementComponent : MonoBehaviour
{    
    #region references
    [SerializeField]
    private Transform myTransform;
    private Rigidbody2D myRigidBody;
    private DeathComponent myDeathComponent;
    [SerializeField]
    private TempoManager TempoManager;
    #endregion

    #region properties
    public float speed;
    #endregion

    private void Start()
    {
        myTransform = transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        myDeathComponent = GetComponent<DeathComponent>();
    }

    #region methods
    public void Autoscroll()
    {
        speed = GameManager.Instance.PlayerSpeed;
        myRigidBody.velocity = Vector2.right * speed;
        myDeathComponent.PlayerCanBeKilled = true;
    }
    public void InitialPosition(Vector3 position)
    {
        myTransform.position = position;
    }
    #endregion
}
