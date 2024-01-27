using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingTrigger : MonoBehaviour
{
    public List<GameObject> stairwayAbove;
    Color inActive = new Color(0, 0, 0, 0.5f);  // Black with 50% alpha.
    bool triggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !triggered)
        {
            foreach (GameObject step in stairwayAbove)
            {
                step.GetComponent<SpriteRenderer>().color = inActive;
                step.GetComponent<BoxCollider2D>().enabled = false;
                triggered = true;
            }
        }
    }
}