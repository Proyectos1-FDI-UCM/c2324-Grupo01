using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectInteractionComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _destroyTime = 5f; //tiempo que tarda en destruir el objeto

    [SerializeField]
    private int _value = 5; //Puntos que suma al jugador
    #endregion
    #region references
    private ActionComponent _playerActionComponent;
    private GameManager _gameManager;
    private ScoreManager _scoreManager;
    private Animator _myAnimator;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D other) //cuando colisiona
    {
        ActionComponent _playerActionComponent = other.GetComponent<ActionComponent>();
        if (_playerActionComponent != null)
        {
            if (_playerActionComponent.isDashing || _playerActionComponent.isStomping)
            {
                _scoreManager.AddObjectPoints(_value);
            }
            else
            {
                _scoreManager.AddObjectPoints(-_value);
            }
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
        _playerActionComponent = FindObjectOfType<ActionComponent>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        _myAnimator = GetComponent<Animator>();
    }
}
