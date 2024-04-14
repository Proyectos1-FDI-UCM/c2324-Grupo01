using UnityEngine;

public class ObjectInteractionComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _destroyTime = 5f; //tiempo que tarda en destruir el objeto

    [SerializeField]
    private int _value = 5; //Puntos que suma al jugador
    #endregion
    #region references
    private ActionComponent _actionComponent;
    private GameManager _gameManager;
    private ScoreManager _scoreManager;
    private Animator _myAnimator;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D other) //cuando colisiona
    {
        ActionComponent _actionComponent = other.GetComponent<ActionComponent>();
        if (_actionComponent != null)
        {
            if (_actionComponent.currentAction == ActionComponent.Action.Sliding 
            || _actionComponent.currentAction == ActionComponent.Action.Stomping 
            || _actionComponent.currentAction == ActionComponent.Action.Dashing)
            {
                _scoreManager.AddPoints(_value,2 );
                //tipo de punto, 0=monedas, 1=enemigo, 2=objeto
            }
            else
            {
                _scoreManager.AddPoints(-_value, 2);
            }
            //MusicManager.Instance.PlaySoundEffect(MusicManager.Instance.boxSound);
            DestroyAnimation();
            Invoke("DestroyObject", _destroyTime);
        }
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    private void DestroyAnimation()
    {
        _myAnimator.SetTrigger("destroy");
    }
    #endregion
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _actionComponent = FindObjectOfType<ActionComponent>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        _myAnimator = GetComponent<Animator>();

        //tipo 0=monedas, 1=enemigo, 2=box, 3=DashCoin
        MaxScoreCalculator.Instance.ObjectRegister(2, _value);
    }
}
