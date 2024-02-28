using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    #region references
    private AudioSource m_AudioSource;
    #endregion

    #region methods
    public void PlaySound()
    {
        m_AudioSource.Play();
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

}
