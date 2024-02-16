using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlideComponent : MonoBehaviour
{
    [SerializeField]
    private float DefaultCollisionSizeX;
    [SerializeField]
    private float DefaultCollisionSizeY;
    [SerializeField]
    private float DefaultCollisionOffsetX;
    [SerializeField]
    private float DefaultCollisionOffsetY;
    [SerializeField]
    private float SlideCollisionSizeX;
    [SerializeField]
    private float SlideCollisionSizeY;
    [SerializeField]
    private float SlideCollisionOffsetX;
    [SerializeField]
    private float SlideCollisionOffsetY;

    BoxCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = this.gameObject.GetComponent<BoxCollider2D>();
        myCollider.offset = new Vector2(DefaultCollisionOffsetX, DefaultCollisionOffsetY);
        myCollider.size = new Vector2(DefaultCollisionSizeX, DefaultCollisionSizeY);
    }

    // Update is called once per frame
    void Update()
    {
        // move to input method later
        myCollider.offset = new Vector2(SlideCollisionOffsetX, SlideCollisionOffsetY);
        myCollider.size = new Vector2(SlideCollisionSizeX, SlideCollisionSizeY);
    }
}
