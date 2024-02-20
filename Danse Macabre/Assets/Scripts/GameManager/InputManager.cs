using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region references
    [SerializeField]
    private ActionComponent _playerActionComponent;
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            _playerActionComponent.Jump();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _playerActionComponent.Stomp();
        }
        else if (Input.GetKeyDown(KeyCode.Z)||Input.GetKeyDown(KeyCode.RightArrow))
        {
            _playerActionComponent.Slide();
        }
        
        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _playerActionComponent.SlideStop();
        }
    }
}
