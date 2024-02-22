using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CoinComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private int _coinValue = 1;
    #endregion

    #region references
    private ScoreManager _points;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprueba si esta colisionando con el personaje
        ActionComponent _player = collision.gameObject.GetComponent<ActionComponent>();
        
        if (_player)
        {
            _points.AddCoinPoints(_coinValue);
            _points.CoinRegister();
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _points = FindObjectOfType<ScoreManager>();
    }

}
