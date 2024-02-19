using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CoinComponent : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprueba si estácolisionando con el personaje (tiene que llevar el inventory component)
        InventoryComponent _playerInventory = collision.gameObject.GetComponent<InventoryComponent>();
        
        if (_playerInventory)
        {
            _playerInventory.CoinRegister();
            Destroy(gameObject);
        }
    }

}
