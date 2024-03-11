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

    public  GameObject _coinText;
    public GameObject _boxText;
    public GameObject _enemyText;
    public GameObject _pinchosText;
    public GameObject _specialCoinText;
    #endregion
    #region parameter
    [SerializeField]
    private float _deactivateTime = 5;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _bigArrowText.SetActive(true);
    }

    public void DisplayTiming(string timing)
    {
        if (timing == "PERFECT") {}
        else if (timing == "GOOD") {}
        else if (timing == "BAD") {}
        else if (timing == "WRONG") {}
        else if (timing == "MISSED") {}
    }

    public void Deactivate()
    {
        _bigArrowText.SetActive(false);
        _smallArrowText.SetActive(true);
    }
    public void Desactivar()
    {
        _coinText.SetActive(false);
        _boxText.SetActive(false);
        _enemyText.SetActive(false);
        _pinchosText.SetActive(false);
        _specialCoinText.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        Invoke("Deactivate", _deactivateTime);
        Invoke("Desactivar", _deactivateTime);
    }
}
