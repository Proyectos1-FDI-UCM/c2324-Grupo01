using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _abovePlatform;
    private AbovePlatform _abovePlatformComponent;
    [SerializeField]
    private GameObject _underPlatform;
    private UnderPlatform _underPlatformComponent;
    [SerializeField]
    private GameObject _colliderObject;
    private BoxCollider2D _collider;
    #endregion

    #region properties
    public bool insidePlatform = false;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionComponent player = collision.gameObject.GetComponent<ActionComponent>();

        if (player != null)
        {
            insidePlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ActionComponent player = collision.gameObject.GetComponent<ActionComponent>();

        if (player != null)
        {
            insidePlatform = false;
        }
    }

    private void Start()
    {
        _abovePlatformComponent = _abovePlatform.GetComponent<AbovePlatform>();
        _underPlatformComponent = _underPlatform.GetComponent<UnderPlatform>();

        _collider = _colliderObject.GetComponent<BoxCollider2D>();
        _collider.enabled = false;
    }


    private void Update()
    {
        if (!insidePlatform && (_underPlatformComponent.under || _abovePlatformComponent.above))
        {
            _collider.enabled = true;
        }
    }
}
