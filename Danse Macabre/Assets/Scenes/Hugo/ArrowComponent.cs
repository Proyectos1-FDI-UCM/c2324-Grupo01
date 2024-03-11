using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowComponent : MonoBehaviour
{
    #region paramaters
    private float perfectRadius = 0.2f;
    private float goodRadius = 0.35f;
    private float badRadius = 0.5f;
    #endregion

    void OnDrawGizmos()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, perfectRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, goodRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + (Vector3)collider.offset, badRadius);
        }
    }
}