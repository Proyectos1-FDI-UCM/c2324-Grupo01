using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollow : MonoBehaviour
{
    #region references
    [SerializeField] private Transform _playerTransform;
    private Transform _textTransform;
    #endregion
    #region parameters
    [SerializeField] private float _verticalOffSet = 670;
    [SerializeField] private float _followFactor= 1.0f;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _textTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 targetPosition = new Vector3(0, _verticalOffSet);
        if (_playerTransform.position.y > 1.0f)
        {
            targetPosition.y += _playerTransform.position.y * 60;

        }
        else targetPosition.y +=_playerTransform.position.y * 100;

        float y = Mathf.Lerp(_textTransform.position.y,targetPosition.y,_followFactor );
        float x = _textTransform.position.x;
        _textTransform.position = new Vector3(x,y,0);
    }
}
