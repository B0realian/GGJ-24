using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingTrigger : MonoBehaviour
{
    public List<GameObject> stairwayAbove;
    Color Active = new Color(1, 1, 1, 1);       // White
    Color inActive = new Color(0, 0, 0, 0.5f);  // Black with 50% alpha.
    bool triggered;
    Transform spawnpoint;
    Transform checkpoint;

    private void Start()
    {
        spawnpoint = GameObject.Find("SpawnPoints").transform;
        checkpoint = transform.GetChild(0).transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !triggered)
        {
            spawnpoint.position = checkpoint.position;
            foreach (GameObject step in stairwayAbove)
            {
                step.GetComponent<SpriteRenderer>().color = inActive;
                step.GetComponent<BoxCollider2D>().enabled = false;
                triggered = true;
            }
        }
        else if (collision.tag == "Player" && triggered)
        {
            foreach (GameObject step in stairwayAbove)
            {
                step.GetComponent<SpriteRenderer>().color = Active;
                step.GetComponent<BoxCollider2D>().enabled = true;
                triggered = false;
            }
        }
    }
}
