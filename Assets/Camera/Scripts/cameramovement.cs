using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramovement : MonoBehaviour
{
    public Transform target;          // Target to follow
    public float smoothSpeed = 0.125f;  // Smoothing speed
    public Vector3 offset;             // Offset from the target
    public float margin = 2.0f;        // Margin before the camera starts to follow

    private void LateUpdate()
    {
        // Calculate the target position with the offset
        Vector3 targetPosition = target.position + offset;

        // Calculate the difference from the current position to the target position
        Vector3 delta = targetPosition - transform.position;

        // Check if the target is outside the margins
        if (Mathf.Abs(delta.x) > margin)
        {
            transform.position = new Vector3(targetPosition.x - Mathf.Sign(delta.x) * margin, transform.position.y, transform.position.z);
        }
        if (Mathf.Abs(delta.y) > margin)
        {
            transform.position = new Vector3(transform.position.x, targetPosition.y - Mathf.Sign(delta.y) * margin, transform.position.z);
        }

        // Smoothly interpolate to the new position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
