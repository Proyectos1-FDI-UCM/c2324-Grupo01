using TMPro;
using UnityEngine;

public class TryTextController : MonoBehaviour
{
    #region references
    [SerializeField] private TextMeshProUGUI _tryNumberText;
    private LifeManager _lifeManager;
    #endregion
    int num;

    private void Start()
    {
        _lifeManager = FindAnyObjectByType<LifeManager>();
    }
    void Update()
    {
        num = _lifeManager.PlayerRemainingLife();
        //num = GameManager.Instance.NumberOfTries() + 1;
        _tryNumberText.text = num.ToString(); // cambia el 1 por el metodo de GameManager que devuelve el int
        
    }
}
