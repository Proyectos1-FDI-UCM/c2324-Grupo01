using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpInfiniteSpawPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject _platformPrefab;


    private void GeneratePlatforms()
    {
        for (int i = 0; i < 50; i++)
        {
            float posX = Random.Range(38 + (i * 15), 38 + (i * 15));
            float posY = Random.Range(0.5f,  2);
            GameObject newPlatform = Instantiate(_platformPrefab, new Vector3 (posX, posY, 0), Quaternion.identity);

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        GeneratePlatforms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
