using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float buttonCTR = 1f;
    #endregion

    #region references
    [SerializeField]
    private List<AudioClip> cursorAudio;
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
