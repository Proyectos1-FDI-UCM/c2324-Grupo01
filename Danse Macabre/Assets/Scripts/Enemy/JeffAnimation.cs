using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeffAnimation : MonoBehaviour
{
    #region references
    private Animator _enemyAnimator;
    private SpriteRenderer _spriteJeff;
    #endregion
    #region methods
    /*public void DeathAnimation()
    {
        _enemyAnimator.SetTrigger("muerte");
    }*/
    #endregion
    void Start()
    {
        _spriteJeff = GetComponent<SpriteRenderer>();
        _spriteJeff.flipX = true;
        _enemyAnimator = GetComponent<Animator>();
    }
}
