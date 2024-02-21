using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbovePlatform : MonoBehaviour
{
    #region references
    private BoxCollider2D _myCollider;
    #endregion

    #region properties
    public bool above = false;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionComponent player = collision.gameObject.GetComponent<ActionComponent>();

        if (player != null)
        {
            above = true;
        }
    }

    void Start()
    {
        _myCollider = GetComponent<BoxCollider2D>();
        _myCollider.enabled = true;
    }

    void Update()
    {
        
    }
}
