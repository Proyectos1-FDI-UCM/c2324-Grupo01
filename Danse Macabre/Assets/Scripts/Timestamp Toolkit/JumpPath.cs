using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPath : MonoBehaviour
{
    private ActionComponent actionComponent;

    public float jumpSpeed = 10f;
    public float horizontalVelocity = 5f;
    public float gravityScaleUpwards = 1f;
    public float gravityScaleDownwards = 2f;
    public int numPoints = 50;
    public float timeBetweenPoints = 0.1f;

    void OnDrawGizmos()
    {
        actionComponent = GetComponent<ActionComponent>();
        //actionComponent

        Vector3 startPosition = transform.position;
        Vector3 previousPoint = startPosition;
        float totalTime = 0f;

        // Calculate gravity taking the different scales into account.
        float gravity = Physics.gravity.y * gravityScaleUpwards;

        for (int i = 0; i < numPoints; i++)
        {
            totalTime += timeBetweenPoints;
            float dx = horizontalVelocity * totalTime;
            float dy = (jumpSpeed * totalTime) + (0.5f * gravity * Mathf.Pow(totalTime, 2));
            Vector3 nextPoint = startPosition + new Vector3(dx, dy, 0);

            // Switch gravity scale at the apex
            if (dy < 0) gravity = Physics.gravity.y * gravityScaleDownwards;

            // Draw the line between the previous point and the next point
            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }
    }

    void Start()
    {
        //_myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
