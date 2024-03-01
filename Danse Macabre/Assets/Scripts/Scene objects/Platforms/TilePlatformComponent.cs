using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlatformComponent : MonoBehaviour
{
    [SerializeField]
    private float constant = 0.1f;

    [SerializeField]
    private GameObject platform;
    private TilemapCollider2D platformCollider;
    [SerializeField]
    private GameObject player;
    private BoxCollider2D playerCollider;

    float feetBottom;
    float platformTop;
    float characterTop;
    float platformBottom;

    private void Start()
    {
        platformCollider = platform.GetComponent<TilemapCollider2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();

        feetBottom = playerCollider.bounds.min.y;
        platformTop = platformCollider.bounds.max.y;
        characterTop = playerCollider.bounds.max.y;
        platformBottom = platformCollider.bounds.min.y;

        //print("top: "+platformTop);
        //print("bottom: "+platformBottom);
        print("top: "+characterTop);
        print("bottom: "+feetBottom);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            print("1");
            print(feetBottom > platformTop + constant);
            if (feetBottom > platformTop + constant || characterTop < platformBottom  + constant)
            {
                print("2");
                platformCollider.isTrigger = false;
            }
            else platformCollider.isTrigger = true;
        }
    }
}
