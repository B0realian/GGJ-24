using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoBodyScript : MonoBehaviour
{
    SimplePiano piano;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (piano == null) piano = GetComponentInParent<SimplePiano>();

        if (collision.gameObject.tag == "Ground" && !piano.collisionDelay) piano.CollidedWithGound();
    }
}
