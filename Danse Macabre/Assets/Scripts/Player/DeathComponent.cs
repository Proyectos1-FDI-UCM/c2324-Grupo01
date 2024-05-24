using UnityEngine;

public class DeathComponent : MonoBehaviour
{
    #region parameters
    int layerValueEnemy;
    int layerValueTraps;
    #endregion

    #region references
    [SerializeField]
    private ScoreManager _scoreManager;
    private MovementComponent _movementComponent;
    private ActionComponent _actionComponent;
    private AnimationComponent _animationComponent;
    private Rigidbody2D _RB;
    #endregion

    #region properties
    private bool playerCanBeKilled = false;
    public bool PlayerCanBeKilled
    {
        get { return playerCanBeKilled; }
        set { playerCanBeKilled = value; }
    }
    #endregion

    private void Start()
    {
        _movementComponent = GetComponent<MovementComponent>();
        _actionComponent = GetComponent<ActionComponent>();
        _RB = GetComponent<Rigidbody2D>();
        _animationComponent = GetComponent<AnimationComponent>();

        layerValueEnemy = LayerMask.NameToLayer("Enemies");
        layerValueTraps = LayerMask.NameToLayer("Traps");
        //_DeathFilter.enabled = false;
        //PlayerAlive = true;
    }
    private void Update()
    {
        CheckVelocityChange();
    }

    #region methods
    /// <summary>
    /// Method that manages death and processes involved.
    /// </summary>
    private void Death()
    {
        if (PlayerCanBeKilled)
        {
            PlayerCanBeKilled = false;
            _RB.velocity = Vector3.zero;
            _animationComponent.ToggleAnimationOff();
            StartCoroutine(GameManager.Instance.PlayerDeath());
        }
    }
    
    /// <summary>
    /// Checks if horizontal velocity of the player's rigidbody has changed he dies.
    /// </summary>
    private void CheckVelocityChange()
    {
        if (_RB.velocity.x < _movementComponent.speed - 0.1f && PlayerCanBeKilled)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // For traps death.
    {
        if (collision.gameObject.layer == layerValueTraps && PlayerCanBeKilled)
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // For enemies and any trigger traps.
    {
        if (collision.gameObject.layer == layerValueTraps && PlayerCanBeKilled)
        {
            Death();
        }
        else if (collision.gameObject.layer == layerValueEnemy 
        && (_actionComponent.currentAction == ActionComponent.Action.Stomping || _actionComponent.currentAction == ActionComponent.Action.Dashing))
        {
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<EnemyAnimation>().DeathAnimation();
            enemy.GetComponent<EnemyInteractionComponent>().DestroyEnemy();

            if (enemy.GetComponent<EnemyInteractionComponent>().BouncyEnemy)
            {
                _actionComponent.Bounce();
            }
        }
        else if (collision.gameObject.layer == layerValueEnemy 
        && !(_actionComponent.currentAction == ActionComponent.Action.Stomping || _actionComponent.currentAction == ActionComponent.Action.Dashing) 
        && PlayerCanBeKilled)
        {
            Death();
        }
    }
    #endregion
}

