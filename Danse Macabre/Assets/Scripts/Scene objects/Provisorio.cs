using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Provisorio : MonoBehaviour
{

    private float timeSpan = 4;
    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timeSpan -= Time.deltaTime;
        if (timeSpan < 0){
            canvas.SetActive(false);
        }
    }
}
