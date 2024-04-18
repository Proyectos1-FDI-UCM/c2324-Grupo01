using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region references
    [SerializeField]
    private ActionComponent _playerActionComponent;
    [SerializeField]
    private PerfectTimingComponent _timing;
    private MenuPausa _menu;
    #endregion

    void Start()
    {
        _menu = GetComponent<MenuPausa>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            _playerActionComponent.Jump();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            _playerActionComponent.Stomp();
        }
        else if (Input.GetKey(KeyCode.Z)||Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _playerActionComponent.SlideDash();
        }
    
        
        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            _playerActionComponent.SlideStop();
        }
        if(Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.P))
        {
            _menu.Pausa();
        }
    }
}
