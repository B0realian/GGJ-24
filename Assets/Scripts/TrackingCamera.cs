using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;

    public float minX;
    public float maxX;
 
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 movePosition = target.position + offset;
        var smoothPos = Vector3.SmoothDamp(transform.position, movePosition, ref this.velocity, damping);

        float capedX = Math.Max(smoothPos.x, minX);
        capedX = Math.Min(capedX, maxX);

        smoothPos.Set(capedX, smoothPos.y, -10);
        transform.position = smoothPos;
    }
}
