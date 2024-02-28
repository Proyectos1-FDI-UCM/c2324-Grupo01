using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class CoinComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private int _coinValue = 1;
    #endregion

    #region references
    private ScoreManager _points;
    private SoundEffectPlayer _sound;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprueba si esta colisionando con el personaje
        ActionComponent _player = collision.gameObject.GetComponent<ActionComponent>();
        
        if (_player)
        {
            _points.AddCoinPoints(_coinValue);
            _points.CoinRegister();
            if (_sound != null)
            {
                _sound.PlaySound();
                Debug.Log("PLayed");
            }
            
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        _points = FindObjectOfType<ScoreManager>();
        _sound = GetComponent<SoundEffectPlayer>();
    }

}
