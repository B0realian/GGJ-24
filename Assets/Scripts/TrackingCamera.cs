using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    public Transform piano;
    Vector3 movePosition;
    Vector3 smoothPos;
    Vector3 velocity = Vector3.zero;

    public float damping = 0.1f;
    private float xPos;
    private float yPos;
    const float zPos = -10;
    const float xCap = 0;
    const float yCap = -33.5f;      // Measured in editor: change value depending on level.

    void FixedUpdate()
    {
        if (piano == null)
            return;
        else
        {
            yPos = piano.position.y;
            if (yPos > -33) xPos = 0;
            else xPos = piano.position.x;

            movePosition = new Vector3(Mathf.Min(xPos, xCap), Mathf.Max(yPos, yCap), zPos);
            Vector3 smoothPos = Vector3.SmoothDamp(transform.position, movePosition, ref this.velocity, damping);
            transform.position = smoothPos;
        }
    }
}
