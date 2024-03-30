using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTextController : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _Text1;
    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private Transform _playerTransform;
    #endregion
    #region parameter
    [SerializeField]
    private float _deactivateTime = 5;
    private float _time;
    #endregion
    void Start()
    {
        _Text1.SetActive(false);
    }

    public void Deactivate()
    {
        _Text1.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (_startPoint.position.x-_playerTransform.position.x<0.1)
        {
            _Text1.SetActive(true);
        }
        if (_time > _deactivateTime)
        {
            Deactivate();
            _time = 0;
        }
        else _time += Time.deltaTime;
    }
}