using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Piano : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float linearFrictionMultiplier;

    private Rigidbody2D rb;
    private const int MAX_HEALTH = 100;
    private int currentHealth;
    public int Health { get => currentHealth; }

    private void OnCollisionEnter2D( Collision2D collision)
    {

        Collider2D[] collidersHit = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f, groundMask);
        int speed = Mathf.CeilToInt(this.rb.velocity.magnitude);
        if (speed > 5)
        {
            this.currentHealth -= speed / collidersHit.Length;
            Debug.Log("current health: " + this.currentHealth);
        }
        Debug.Log(this.rb.velocity.magnitude);
        this.rb.velocity *= this.linearFrictionMultiplier;
        if( this.currentHealth < 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    

    // Start is called before the first frame update
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        currentHealth = MAX_HEALTH;

    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
