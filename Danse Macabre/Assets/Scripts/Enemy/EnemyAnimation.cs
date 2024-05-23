using UnityEngine;


public class EnemyAnimation : MonoBehaviour
{
    #region references
    private Animator _enemyAnimator;
    #endregion

    #region methods
    public void DeathAnimation()
    {
        _enemyAnimator.SetTrigger("muerte");
    }
    #endregion
    void Start()
    {
        _enemyAnimator = GetComponent<Animator>();
    }
}
