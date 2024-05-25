using UnityEngine;
using UnityEngine.UI;

public class ScreenBeatComponent : MonoBehaviour
{
    private ComboManager _combo;
    private TempoManager _tempo;
    private MusicManager _music;
    [SerializeField]
    private Image _image;

    // Colores correspondientes a cada multiplicador.
    [SerializeField]
    Color Multiplier1Color;
    [SerializeField]
    Color Multiplier2Color;
    [SerializeField]
    Color Multiplier3Color;
    [SerializeField]
    Color Multiplier4Color;

    // Valores del 1 al 0 correspondientes a cada multiplicador. Cuanto m�s cerca del 1, m�s visible ser�
    [SerializeField]
    float Multiplier1Intensity = 0.25f;
    [SerializeField]
    float Multiplier2Intensity = 0.50f;
    [SerializeField]
    float Multiplier3Intensity = 0.75f;
    [SerializeField]
    float Multiplier4Intensity = 1f;

    [SerializeField]
    float BeatDurationMultiplier = 0.5f; // Multiplicador a la duraci�n total de cada iteraci�n del efecto (multiplica a Time.deltaTime)

    Color targetColor;
    float baseAlpha = 0.1f;
    float maxAlpha = 0;
    float currentAlpha;
    bool combo = false;
    public bool Combo
    {
        get { return combo; }
        set { combo = value; }
    }

    void Start()
    {
        _combo = FindObjectOfType<ComboManager>();
        _tempo = FindObjectOfType<TempoManager>();
        _music = FindObjectOfType<MusicManager>();
    }

    void Update()
    {
        if (combo) Beat();
        else ResetColor();
    }

    public void Beat()
    {
        float time = _music.GetTime();

        float aux = time % _tempo.SecondsPerTick; // integer
        string formattedNumber = "0." + aux.ToString(); // decimal string
        float betweenBeats = float.Parse(formattedNumber); // convert to float 0.integer
        float interpolate = betweenBeats/_tempo.SecondsPerTick;

        targetColor = TargetColor();
        currentAlpha = Mathf.Clamp(interpolate, baseAlpha, maxAlpha - 0.1f);
        _image.color = new Color(targetColor.r, targetColor.g, targetColor.b, currentAlpha);
    }

    private void ResetColor()
    {
        _image.color = new Color(0, 0, 0, 0);
    }

    private Color TargetColor()
    {
        if (_combo.multiplier < 2)
        {
            maxAlpha = Multiplier1Intensity;
            targetColor = new Color(Multiplier1Color.r, Multiplier1Color.g, Multiplier1Color.b, Multiplier1Intensity);
        }
        else if (_combo.multiplier >= 4)
        {
            maxAlpha = Multiplier4Intensity;
            targetColor = new Color(Multiplier4Color.r, Multiplier4Color.g, Multiplier4Color.b, Multiplier4Intensity);
        }
        else if (_combo.multiplier >= 3)
        {
            maxAlpha = Multiplier3Intensity;
            targetColor = new Color(Multiplier3Color.r, Multiplier3Color.g, Multiplier3Color.b, Multiplier3Intensity);
        }
        else if (_combo.multiplier >= 2)
        {
            maxAlpha = Multiplier2Intensity;
            targetColor = new Color(Multiplier2Color.r, Multiplier2Color.g, Multiplier2Color.b, Multiplier2Intensity);
        }

        return targetColor;
    }
}