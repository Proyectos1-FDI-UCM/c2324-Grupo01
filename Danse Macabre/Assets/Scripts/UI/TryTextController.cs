using TMPro;
using UnityEngine;

public class TryTextController : MonoBehaviour
{
    #region references
    [SerializeField] private TextMeshProUGUI _tryNumberText;
    #endregion
    int num;

    void Update()
    {
        num = GameManager.Instance.PlayerRemainingLife();
        //num = GameManager.Instance.NumberOfTries() + 1;
        _tryNumberText.text = num.ToString(); // cambia el 1 por el metodo de GameManager que devuelve el int
        
    }
}
