using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderPlatform : MonoBehaviour
{
    #region references
    private BoxCollider2D _myCollider;
    [SerializeField]
    private AbovePlatform _abovePlatformComponent;
    #endregion

    #region properties
    public bool under = false;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionComponent player = collision.gameObject.GetComponent<ActionComponent>();

        if (player != null)
        {
            under = true;
            _abovePlatformComponent.Under();
            Debug.Log("Below platform");
        }
    }

    public void Above()
    {
        under = false;
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
