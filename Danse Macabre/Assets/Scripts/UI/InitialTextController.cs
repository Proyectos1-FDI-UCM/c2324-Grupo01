using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTextController : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _bigArrowText;
    [SerializeField]
    private GameObject _smallArrowText;
    #endregion
    #region parameter
    [SerializeField]
    private float _deactivateTime = 5;
    #endregion
    void Start()
    {
        _bigArrowText.SetActive(true);
    }

    public void Deactivate()
    {
        _bigArrowText.SetActive(false);
        _smallArrowText.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        Invoke("Deactivate", _deactivateTime);
    }
}
