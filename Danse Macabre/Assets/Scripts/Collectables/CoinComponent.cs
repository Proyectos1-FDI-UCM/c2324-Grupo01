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
    private PointsCounterComponent _points;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprueba si está colisionando con el personaje (tiene que llevar el inventory component)
        InventoryComponent _playerInventory = collision.gameObject.GetComponent<InventoryComponent>();
        
        if (_playerInventory)
        {
            _points.AddPoints(_coinValue);
            _playerInventory.CoinRegister();
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _points = FindObjectOfType<PointsCounterComponent>();
    }

}
