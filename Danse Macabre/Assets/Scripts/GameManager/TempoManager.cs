using UnityEngine;
using UnityEngine.SceneManagement;

public class TempoManager : MonoBehaviour
{
    [SerializeField]
    private LevelDataLoader _levelDataLoader;
    [SerializeField]
    int BPM = 60; // BPM de la canci�n del nivel. Serializado
    [SerializeField]
    float TilesPerTick = 4; // Cantidad de tiles que hay que recorrer en cada tick de la canci�n. Serializado

    public float SecondsPerTick; // Cantidad de tiempo para completar un tick. Referenciable
    public float PlayerSpeed; // Velocidad del jugador calculada basado en BPM y TilesPerTick. Referenciable



    /// <summary>
    /// Calculas the player speed base on setted parameters.
    /// </summary>
    public void UpdatePlayerSpeedInInspector()
    {
        SecondsPerTick = 60f / BPM;
        PlayerSpeed = TilesPerTick / SecondsPerTick;
        Debug.Log("SPT: " + SecondsPerTick + " / PlayerSpeed: " + PlayerSpeed);
    }

    /// <summary>
    /// Register in data container the player speed and level associated.
    /// </summary>
    public void SetLevelTempo()
    {
        _levelDataLoader = GetComponent<LevelDataLoader>();
        string sceneName = SceneManager.GetActiveScene().name;
        _levelDataLoader.SaveLevelDataInContainer(sceneName, PlayerSpeed);
    }


}
