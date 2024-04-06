using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private Image image;
    private void ChangeColor()
    {
        image.color = Color.red;
    }
    void Start()
    {
        image = GetComponent<Image>();
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
