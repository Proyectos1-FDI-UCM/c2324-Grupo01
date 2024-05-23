using UnityEngine;

public class CoinComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private int _coinValue = 10;
    #endregion

    #region references
    private ScoreManager _points;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprueba si esta colisionando con el personaje
        ActionComponent _player = collision.gameObject.GetComponent<ActionComponent>();
        if (_player)
        {
            //tipo de punto, 0=monedas, 1=enemigo, 2=objeto, 3=MonedaDash(MaxScoreCalculator)
            _points.AddPoints(_coinValue, 0);

            Destroy(gameObject);
        }
    }
    #endregion

    void Start()
    {
        _points = FindObjectOfType<ScoreManager>();
        if (_coinValue > 15) MaxScoreCalculator.Instance.ObjectRegister(3,_coinValue);
        else MaxScoreCalculator.Instance.ObjectRegister(1, _coinValue);
    }

}
