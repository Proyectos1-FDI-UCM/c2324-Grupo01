using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFPS : MonoBehaviour
{
    [SerializeField]
    private int targetFPS = 50;

    void Awake()
    {
        Application.targetFrameRate = targetFPS;
    }
}
