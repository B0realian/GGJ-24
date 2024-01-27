using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public delegate void OnDroppedAndStationary(Piano piano);

public class Piano : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float frictionMultiplier;

    [SerializeField] private float damageMultiplier = 1f;

    private Rigidbody2D rb;
    [SerializeField] private const int MAX_HEALTH = 100;
    [SerializeField] private int currentHealth;
    public int Health { get => currentHealth; }

    private AudioSource audioSource;
    [SerializeField] private AudioClip hittingSound;
    [SerializeField] private AudioClip breakingSound;

    private event OnDroppedAndStationary OnDropped;

    public void Subscribe(OnDroppedAndStationary handler)
    {
        OnDropped += handler;
    }

    public void Unsubscribe(OnDroppedAndStationary handler)
    {
        OnDropped -= handler;
    }

    private void PlaySound(AudioClip clip)
    {
        if(clip != null)
        {
            if (this.audioSource.isPlaying)
            {
                this.audioSource.Stop();
            }
            this.audioSource.clip = clip;
            this.audioSource.Play();
        }
    }
    private void OnCollisionEnter2D( Collision2D collision)
    {
        if (1 << collision.gameObject.layer == groundMask.value)
        {
            float speed = this.rb.velocity.magnitude;
            PlaySound(this.hittingSound);
            this.rb.velocity *= this.frictionMultiplier;
            //Debug.Log("current health: " + this.currentHealth);
            this.currentHealth -= Mathf.RoundToInt(this.damageMultiplier * speed);
            if( this.currentHealth < 0)
            {
                //Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(Game.Instance.IsGameRunning && 1 << collision.gameObject.layer == groundMask.value && HasStopped())
        {
            this.OnDropped?.Invoke(this);
            Debug.Log("tjena");
        }
    }

    private void OnDestroy()
    {
        // play piano breaking sound
        PlaySound(this.breakingSound);
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.audioSource = this.GetComponent<AudioSource>();
        this.currentHealth = MAX_HEALTH;

    }

    public Boolean HasStopped()
    {
        return this.rb.velocity.magnitude < 0.001f;
    }

    public void ResetPiano(Vector2 position)
    {
        if(audioSource != null)
        {
            this.audioSource.Stop();
        }
        this.rb.velocity = Vector3.zero;
        this.rb.position = position;
        this.rb.rotation = 0f;
        this.currentHealth = MAX_HEALTH;
    }

}
