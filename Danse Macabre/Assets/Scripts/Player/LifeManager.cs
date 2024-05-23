using UnityEngine;

public class LifeManager : MonoBehaviour
{
    #region paramenters
    private int playerMaxLife = 4;
    #endregion paramenters

    #region properties
    private static int playerRemainingLife;
    //private static int numberOfTries = 0;
    #endregion properties


    // TRIES
    // public int NumberOfTries()
    // {
    //     return numberOfTries;
    // }
    // public void IncrementTries()
    // {
    //     numberOfTries += 1;
    // }
    // public void ResetTries()
    // {
    //     numberOfTries = 0;
    // }

    // LIFES
    public bool PlayerHasLife()
    {
        return playerRemainingLife > 0;
    }
    public void PlayerLosesLife()
    {
        playerRemainingLife -= 1;
    }
    public void ResetPlayerLife()
    {
        playerRemainingLife = playerMaxLife;
    }
    public int PlayerRemainingLife()
    {
        return playerRemainingLife;
    }

}
