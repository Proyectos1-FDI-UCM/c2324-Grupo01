using UnityEngine;
using UnityEngine.UI;

public class DeathFilterColor : MonoBehaviour
{
    [SerializeField]
    private Image _DeathFilter;

    private int c;

    [SerializeField]
    private Color[] Color;


    void Start()
    {
        c = 0;
    }

    public void ColorChange()
    {        

        if(c < 4) //cycles through 4 different colors. can be changed if you add more colors just increasing the counter but time needs to be adjusted manually
        {
            _DeathFilter.color = Color[c];
            //Debug.Log("c: " + c + "  " + Time.time);
            c++;
            Invoke("ColorChange", 0.3f); //change every 0.3 seconds
        }
        
    }
}
