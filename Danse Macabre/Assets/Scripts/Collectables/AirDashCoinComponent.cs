using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashCoinComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _canDashTime = 2.0f;
    #endregion

    #region methods 
    //cuanto lo colecciono llamo a la corrutina del dash
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionComponent _player = collision.gameObject.GetComponent<ActionComponent>();
        if (_player)
        {
            _player.DashCountDown(_canDashTime);
        }
    }
    #endregion 
}
