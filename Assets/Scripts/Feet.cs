using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    CircleCollider2D circleCollider;
    public float checkRadius = 0.05f;
    public float activationDelay = 1;
    Transform leftFoot;
    Transform rightFoot;
    const int GROUNDLAYER = 256;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        leftFoot = transform.GetChild(0);
        rightFoot = transform.GetChild(1);
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(leftFoot.position, checkRadius, GROUNDLAYER)
            || Physics2D.OverlapCircle(rightFoot.position, checkRadius, GROUNDLAYER))
        {
            circleCollider.enabled = false;
            Invoke("ActivateCollider", activationDelay);
        }
    }

    private void ActivateCollider()
    {
        circleCollider.enabled = true;
    }
}
