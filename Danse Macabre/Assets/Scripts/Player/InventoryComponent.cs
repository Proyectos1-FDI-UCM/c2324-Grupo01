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
}
