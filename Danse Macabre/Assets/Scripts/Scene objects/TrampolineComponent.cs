using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float impulseTrampolin = 12;
    #endregion

    #region references
    private Rigidbody2D _playerRB;
    private ActionComponent _playerActionComponent;
    #endregion


    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionComponent _player = collision.GetComponent<ActionComponent>();

        if (_player!=null)
        {

            if (_playerActionComponent.isStomping)
            {
                _playerRB.velocity = new Vector2(_playerRB.velocity.x, 0);
                _playerRB.AddForce(impulseTrampolin * Vector2.up, ForceMode2D.Impulse);
                _playerActionComponent.isStomping = false;
            }
        }

    }
    #endregion

    void Start()
    {
        _playerActionComponent = FindObjectOfType<ActionComponent>();
        _playerRB = FindObjectOfType<Rigidbody2D>();
    }
}
