using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataLoader : MonoBehaviour
{
    #region data reference
    [SerializeField]
    private LevelDataContainer levelData;
    #endregion

    /// <summary>
    /// Gets player speed stored in level data scriptable object for the particular scene active.
    /// </summary>
    /// <returns>player speed</returns>
    public float GetCurrentScenePlayerSpeed()
    {
        float playerSpeed = 0;
        string sceneName = SceneManager.GetActiveScene().name;

        int i = 0;
        bool match = false;
        while (i < levelData.Levels.Count && !match)
        {
            var data = levelData.Levels[i];
            if (sceneName == data.sceneName) 
            {
                playerSpeed = data.playerSpeed;
                match = true;
            }
            i++;
        }

        if (!match) Debug.LogError("Nombre de la escena no encontrada en el level data container!!!");

        return playerSpeed;
    }

    /// <summary>
    /// Saves scene name and player speed associated in level data container.
    /// </summary>
    /// <param name="p_sceneName"></param>
    /// <param name="p_playerSpeed"></param>
    public void SaveLevelDataInContainer(string p_sceneName, float p_playerSpeed)
    {
        int numberOfLevelsStored = levelData.Levels.Count;
        bool levelStored = false;

        for (int i = 0; i < numberOfLevelsStored; i++)
        {
            if (levelData.Levels[i].sceneName == p_sceneName) 
            {
                var temp = levelData.Levels[i];
                temp.playerSpeed = p_playerSpeed;
                levelData.Levels[i] = temp;
                levelStored = true;
                Debug.Log("Level already stored. Player speed updated!");
            }
        }
        if (!levelStored)
        {
            levelData.Levels.Add(new LevelDataContainer.LevelData
                {
                    sceneName = p_sceneName,
                    playerSpeed = p_playerSpeed
                    
                });
                Debug.Log("New level stored!");
        }
    }

}
