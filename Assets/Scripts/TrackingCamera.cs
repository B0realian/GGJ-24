using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 movePosition = target.position + offset;
        var smoothPos = Vector3.SmoothDamp(transform.position, movePosition, ref this.velocity, damping);
        smoothPos.Set(smoothPos.x, smoothPos.y, -10);

        transform.position = smoothPos;
    }
}
