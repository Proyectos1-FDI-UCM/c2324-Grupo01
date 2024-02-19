using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    #region parameters
    private int _nCoins = 0;
    #endregion

    #region methods
    public void CoinRegister()
    {
        _nCoins++;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("monedas: " + _nCoins);
    }
}
