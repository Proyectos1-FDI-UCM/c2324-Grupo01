using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    #region parameters
    //variable que controla el sonido del boton
    [SerializeField]
    private float buttonCTR = 1f;
    #endregion

    #region references
    //audioclip para on pointer enters
    [SerializeField]
    private List<AudioClip> cursorAudio;
    //audioclip para on button click
    [SerializeField]
    private AudioClip selected;
    #endregion
    public void OnPointerEnters()
    {
        int i = Random.Range(0, cursorAudio.Count);
        MusicManager.Instance.PlaySoundEffect(cursorAudio[i],buttonCTR);
    }
    public void OnButtonClick()
    {
        MusicManager.Instance.PlaySoundEffect(selected,buttonCTR);
    }
}
