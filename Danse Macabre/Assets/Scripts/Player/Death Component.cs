using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    #region references
    private GameManager _gameManager;
    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MovementComponent>()) 
        {
            _gameManager.Muerte();
        }
        
    }
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }
    void Update()
    {
        
    }
}
