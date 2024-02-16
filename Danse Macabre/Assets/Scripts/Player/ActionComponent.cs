using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionComponent : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    #endregion

    //CAMBIAD LO QUE HACE CADA METODO!! (LO QUE HAY ES PARA PROBAR!)
    public void Jump()
    {
        gameObject.SetActive(false);
    }
    public void Stomp()
    {
        gameObject.SetActive(true);
    }
    public void Slide()
    {
        _myTransform.localScale = new Vector3(0.5f, 0.5f, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
