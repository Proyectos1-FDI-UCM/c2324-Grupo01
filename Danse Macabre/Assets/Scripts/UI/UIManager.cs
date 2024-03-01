using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        _bigArrowText.SetActive(true);
        _smallArrowText.SetActive(false);
    }
    public void DeactivateExplication()
    {
        _bigArrowText.SetActive(false);
        _smallArrowText.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        Invoke("DeactivateExplication", _deactivateTime);
    }
}
