using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

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
            _points.AddCoinPoints(_coinValue);
            _points.CoinRegister();
            MusicManager.Instance.PlaySoundEffect(MusicManager.Instance.coinSound);
            //MusicManager2.Instance.PlaySoundEffect(MusicManager2.Instance.coinSound);

            Destroy(gameObject);
        }
    }
    #endregion

    void Start()
    {
        _points = FindObjectOfType<ScoreManager>();
    }

}
