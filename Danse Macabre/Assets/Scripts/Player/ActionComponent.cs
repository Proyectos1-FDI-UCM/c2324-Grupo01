using System.Collections;
using UnityEngine;


public class ActionComponent : MonoBehaviour
{
    #region parameters
    bool debugThis = false;
    [SerializeField]
    public float jumpSpeed = 11;
    public float originalGravityScale;
    [SerializeField]
    public float gravityFactor = 0.90f;
    private float groundCheckDistance = 0.55f;
    private float trampolinCheckDistance = 0.60f;
    private float enemyCheckDistance = 0.75f;
    public float stompDownwardSpeed = 20;
    public float trampolineJumpSpeed = 15;
    [SerializeField]
    private float dashDuration = 0.7f;
    private float dashEndTime = 0;

    //rango de tiempo que puedes hacer dash (cuando coges la moneda especial)
    [SerializeField]
    private float _dashElapsedTime = 0;

    [SerializeField]
    private float slideCTR = 0.15f;
    [SerializeField]
    private float dashCTR = 0.2f;
    [SerializeField]
    private float jumpCTR = 0.2f;
    [SerializeField]
    private float stompCTR = 0.1f;
    #endregion

    #region references
    private PathSaver pathSaver;
    private Transform _myTransform;
    private Rigidbody2D _myRB;
    private BoxCollider2D myCollider;
    [SerializeField]
    private LayerMask groundLayer; // Capa que representa el suelo / Añadir trampoline!
    [SerializeField]
    private LayerMask trampolineLayer;
    [SerializeField]
    private LayerMask enemyLayer;
    private PerfectTimingComponent timingComponent;

    // sfx
    [SerializeField] 
    private AudioClip _jumpSound;
    [SerializeField]
    private AudioClip _stompSound;
    [SerializeField]
    private AudioClip _slideSound;
    [SerializeField]
    private AudioClip _dashSound;
    
    //collider references
    [SerializeField]
    private float DefaultCollisionSizeX;
    [SerializeField]
    private float DefaultCollisionSizeY;
    [SerializeField]
    private float DefaultCollisionOffsetX;
    [SerializeField]
    private float DefaultCollisionOffsetY;
    [SerializeField]
    private float SlideCollisionSizeX;
    [SerializeField]
    private float SlideCollisionSizeY;
    [SerializeField]
    private float SlideCollisionOffsetX;
    [SerializeField]
    private float SlideCollisionOffsetY;

    [SerializeField]
    private ParticleSystem SlideParticleSystem;
    private ParticleSystem.EmissionModule SlideParticleEmitter;
    [SerializeField]
    private ParticleSystem StompParticleSystem;
    private ParticleSystem.EmissionModule StompParticleEmitter;
    [SerializeField]
    private ParticleSystem DashParticleSystem;
    private ParticleSystem.EmissionModule DashParticleEmitter;

    private AudioSource myAudioSource;
    #endregion

    #region properties
    public enum Action {Running, Falling, Jumping, Stomping, Sliding, Dashing, Null}; // enum for every posible action.
    public Action currentAction; // stores the current action being performed.
    private bool canDash = false;
    #endregion

    #region methods
    /// <summary>
    /// Method to check if the player is grounded.
    /// Applied to Ground, Platform and Trampoline layers.
    /// </summary>
    /// <returns>True if grounded.</returns>
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(_myTransform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    /// <summary>
    /// Checks the collision with the trampoline layer.
    /// </summary>
    /// <returns>Returns true if the collision was with a trampoline.</returns>
    private bool IsTrampolin()
    {
        RaycastHit2D hit = Physics2D.Raycast(_myTransform.position, Vector2.down, trampolinCheckDistance, trampolineLayer);
        return hit.collider != null;
    }

    /// <summary>
    /// Sets an upward velocity to the player (but fist a code needs to check if isTrampoline() and is stomping)
    /// and sets the current action to jumping.
    /// </summary>
    private void TrampolineBounce()
    {
        _myRB.velocity = new Vector3(_myRB.velocity.x, trampolineJumpSpeed);
        //pathSaver.StartSaving();
    }

    /// <summary>
    /// Check if the player is grounded before performs a jump. The current action is changed in the Bounce() method.
    /// </summary>
    public void Jump()
    {
        timingComponent.CheckNearbyArrow(Action.Jumping); // Sends jumping to perfect timing.

        if (IsGrounded())
        {
            myAudioSource.PlayOneShot(_jumpSound, jumpCTR);
            //pathSaver.StartSaving();
            Bounce();
        }
    }

    /// <summary>
    /// Este método est?separado para que se pueda llamar a la hora de rebotar encima de un enemigo determinado sin comprobar grounded.
    /// </summary>
    public void Bounce()
    {
        _myRB.velocity = new Vector2(_myRB.velocity.x, jumpSpeed);
        currentAction = Action.Jumping;
    }

    /// <summary>
    /// Performs a rapid descent, but checks if it is not already doing it (not stomping).
    /// </summary>
    public void Stomp()
    {
        timingComponent.CheckNearbyArrow(Action.Stomping); // Sends stomping to perfect timing.

        if (!IsGrounded() && currentAction != Action.Stomping)
        {
            currentAction = Action.Stomping;
            _myRB.velocity = new Vector3(_myRB.velocity.x, -stompDownwardSpeed);
            //pathSaver.StartSaving();
            
            myAudioSource.PlayOneShot(_stompSound, stompCTR);
        }
    }

    /// <summary>
    /// Method for Slide an Dash.
    /// If the player is grounded a slide is performed. If in the air and can dash due to a dash coin, performs a dash.
    /// </summary>
    public void SlideDash()
    {
        // To perform a Dash (Air):
        if (!IsGrounded() && canDash)
        {
            if (currentAction != Action.Dashing) timingComponent.CheckNearbyArrow(Action.Dashing); // Sends, only once, dashing to perfect timing.
            currentAction = Action.Dashing;

            canDash = false; // So dash can't trigger multiple times while pressing the input button.
            _dashElapsedTime = 0; // So the coin the same coin won't set canDash to true again in update().

            StartCoroutine(Dash()); // Coroutine to perform the dash for a period of time.

            if (!myAudioSource.isPlaying) myAudioSource.PlayOneShot(_dashSound, dashCTR);
        }

        // To perform a Slide (Ground):
        else if (IsGrounded())
        {
            if (currentAction != Action.Sliding){
                currentAction = Action.Sliding;
                timingComponent.CheckNearbyArrow(currentAction); // Sends, only once, sliding to perfect timing.
                // Inside this IF so it changes collider only once:
                myCollider.offset = new Vector2(SlideCollisionOffsetX, SlideCollisionOffsetY);
                myCollider.size = new Vector2(SlideCollisionSizeX, SlideCollisionSizeY);
            }

            if (!myAudioSource.isPlaying)
            {
                myAudioSource.PlayOneShot(_slideSound, slideCTR);
                myAudioSource.loop = true;
            }
        }
    }

    /// <summary>
    /// Stops sliding: collider set to original parameters and action to running if grounded.
    /// </summary>
    public void SlideStop()
    {
        if (currentAction == Action.Sliding){ // Changes colliders only if necessary.
            myCollider.offset = new Vector2(DefaultCollisionOffsetX, DefaultCollisionOffsetY);
            myCollider.size = new Vector2(DefaultCollisionSizeX, DefaultCollisionSizeY);
        }

        if (IsGrounded()) currentAction = Action.Running; // If on the ground, the only possible state after quitting sliding is running.
        else if (currentAction == Action.Dashing) currentAction = Action.Falling; // If there's no longer input for dash the player falls.

        myAudioSource.Stop();
        myAudioSource.loop = false;
    }

    /// <summary>
    /// Method that sets a countdown to perform a dash. The action is available for the parameter specified seconds.
    /// </summary>
    /// <param name="_time"></param>
    public void DashCountDown(float _time)
    {
        _dashElapsedTime = _time;
    }
    #endregion

    #region interface
    /// <summary>
    /// Dash action that is performed for a dash duration time.
    /// Changes the gravity scale of the rigidbody to the player won't move in the y axis.
    /// After the action, the gravity scale is set to it's original value.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dash()
    {
        float startTime = Time.time;
        dashEndTime = startTime + dashDuration;

        _myRB.velocity = new Vector2(_myRB.velocity.x, 0);
        _myRB.gravityScale = 0;

        while (Time.time < dashEndTime && currentAction == Action.Dashing)
        {
            yield return new WaitForFixedUpdate();
        }

        _myRB.gravityScale = originalGravityScale;

        if (currentAction != Action.Stomping) currentAction = Action.Falling; // Changes to falling if dash time ends and there's no other action being made.

        myAudioSource.Stop();
    }
    #endregion

    private void Awake()
    {
        SlideParticleSystem.Play();
        SlideParticleEmitter = SlideParticleSystem.emission;
        SlideParticleEmitter.enabled = false;

        StompParticleSystem.Play();
        StompParticleEmitter = StompParticleSystem.emission;
        StompParticleEmitter.enabled = false;

        DashParticleSystem.Play();
        DashParticleEmitter = DashParticleSystem.emission;
        DashParticleEmitter.enabled = false;
    }

    void Start()
    {
        _myTransform = transform;
        _myRB = GetComponent<Rigidbody2D>();
        
        myCollider = this.gameObject.GetComponent<BoxCollider2D>();
        myCollider.offset = new Vector2(DefaultCollisionOffsetX, DefaultCollisionOffsetY);
        myCollider.size = new Vector2(DefaultCollisionSizeX, DefaultCollisionSizeY);

        timingComponent = GetComponent<PerfectTimingComponent>();

        pathSaver = GetComponent<PathSaver>();

        originalGravityScale = _myRB.gravityScale;
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {        
        Action lastAction = currentAction; // To further call SlideStop() if current action is changed by the code bellow.

        if (_myRB.velocity.y > 0.1f){ // If the velocity y if greater then 0.1f it means the player is jumping.
            currentAction = Action.Jumping;
        }
        else if (!IsGrounded() && _myRB.velocity.y < -0.1f && currentAction != Action.Stomping){
            currentAction = Action.Falling; // The player is falling if it's not in the ground and not stomping.
        }
        else if (IsTrampolin() && currentAction == Action.Stomping){ // If hits a trampoline while stomping calls TrampolineBounce and sets action to jumping.
            TrampolineBounce();
            //print("depois tramp: "+ _myTransform.position);
            currentAction = Action.Jumping;
        }
        else if (IsGrounded() && currentAction != Action.Sliding){ // If is in the ground and not sliding it's definitely running.
            currentAction = Action.Running;
        }


        if (lastAction == Action.Sliding && currentAction != Action.Sliding){ // Stops the slide if the current action changed from sliding.
            SlideStop();
        }


        // Code to change gravity factor for when is going up or down if not dashing.
        if (currentAction != Action.Dashing)
        {
            if (_myRB.velocity.y < 0) // Gravity going down.
            {
                _myRB.gravityScale = gravityFactor * originalGravityScale;
            }
            else{                     // Gravity going up.
                _myRB.gravityScale = originalGravityScale;
            }
        }
        
        //Despues de coger la moneda, dash elapsed time se actualiza y entrar?al condicional
        if (_dashElapsedTime > 0)
        {
            _dashElapsedTime -= Time.deltaTime;
            canDash = true;
        }
        else {
            canDash = false;
        }

        // VFX
        if (currentAction == Action.Sliding)
        {
            SlideParticleEmitter.enabled = true;
        }
        else
        {
            SlideParticleEmitter.enabled = false;
        }

        if (currentAction == Action.Stomping)
        {
            StompParticleEmitter.enabled = true;
        }
        else
        {
            StompParticleEmitter.enabled = false;
        }

        if (currentAction == Action.Dashing)
        {
            DashParticleEmitter.enabled = true;
        }
        else
        {
            DashParticleEmitter.enabled = false;
        }

    }

    /// <summary>
    /// Method to control bot's dash.
    /// </summary>
    /// <param name="p_canDash"></param>
    public void BotCanDash(bool p_canDash)
    {
        canDash = p_canDash;
    }
    
    public float GetDashDuration()
    {
        return dashDuration;
    }
}
