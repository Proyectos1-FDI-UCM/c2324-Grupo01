using UnityEngine;


public class EnemyInteractionComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _destroyTime = 0.5f;

    [SerializeField]
    private int _enemyValue = 5;
    #endregion

    #region references
    private ScoreManager _scoreManager;
    [SerializeField]
    public bool BouncyEnemy;
    [SerializeField] Collider2D Collider;
    #endregion

    /// <summary>
    /// Add score and invokes method to destroy enemy object.
    /// </summary>
    public void DestroyEnemy()
    {
        //tipo de punto, 0=monedas, 1=enemigo, 2=objeto
        _scoreManager.AddPoints(_enemyValue, 1);
        Invoke("DestroyGameObject", _destroyTime);

    }

    /// <summary>
    /// Destroy enemy object.
    /// </summary>
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();

        //tipo de punto, 0=monedas, 1=enemigo, 2=objeto
        MaxScoreCalculator.Instance.ObjectRegister(1, _enemyValue);
    }
}
