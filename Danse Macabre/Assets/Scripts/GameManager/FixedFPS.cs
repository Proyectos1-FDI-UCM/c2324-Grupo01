using UnityEngine;

/// <summary>
/// Fixes game FPS in 50.
/// </summary>
public class FixedFPS : MonoBehaviour
{
    [SerializeField]
    private int targetFPS = 50;

    void Awake()
    {
        Application.targetFrameRate = targetFPS;
    }
}
