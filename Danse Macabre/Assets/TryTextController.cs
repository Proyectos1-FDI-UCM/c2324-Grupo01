using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TryTextController : MonoBehaviour
{
    #region references
    [SerializeField] private TextMeshProUGUI _tryNumberText;
    #endregion

    // Update is called once per frame
    void Update()
    {
        _tryNumberText.text = GameManager.Instance.NumberOfTries().ToString(); // cambia el 1 por el metodo de GameManager que devuelve el int
        
    }
}
