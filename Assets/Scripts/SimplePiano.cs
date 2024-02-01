using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimplePiano : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite mint;
    public Sprite good;
    public Sprite lightlyPlayed;
    public Sprite heavilyPlayed;

    public float collisionTimer = 4.0f;
    public float spawnDelay = 1.0f;
    private float _collisionTime;

    public int health;
    public int maxHealth = 4;

    bool collisionDelay = false;
    bool gameOver = false;

    TrackingCamera cam;
    GameObject moverA;
    GameObject moverB;
    Transform spawnpoint;

    TextMeshProUGUI infoText;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<TrackingCamera>();
        cam.piano = this.transform;
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = mint;
        moverA = GameObject.Find("PlayerLeft(Clone)");
        moverB = GameObject.Find("PlayerRight(Clone)");
        spawnpoint = GameObject.Find("SpawnPoints").transform;
        infoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (collisionDelay)
        {
            _collisionTime += Time.deltaTime;
            if (_collisionTime > collisionTimer)
            {
                collisionDelay = false;
                _collisionTime = 0;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !collisionDelay)
        {
            collisionDelay = true;
            health--;
            FindSprite(health);
            if (!gameOver)
            {
                Invoke("RespawnMovers", spawnDelay);
                Invoke("RespawnPiano", spawnDelay + 2);
            }
        }
    }

    private void FindSprite(int health)
    {
        switch (health) 
        {
            case 3:
                spriteRenderer.sprite = good;
                break;
            case 2:
                spriteRenderer.sprite = lightlyPlayed;
                break;
            case 1:
                spriteRenderer.sprite= heavilyPlayed;
                break;
            case 0:
                gameOver = true;
                moverA.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                moverB.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                infoText.enabled = true;
                infoText.text = "GAME OVER MAN!";
                Invoke("BackToMenu", 3);
                break;

            default: break;
        }
    }

    private void RespawnMovers()
    {
        spriteRenderer.enabled = false;
        moverA.transform.position = spawnpoint.transform.GetChild(0).transform.position;
        moverA.transform.rotation = Quaternion.identity;
        moverA.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        moverB.transform.position = spawnpoint.transform.GetChild(1).transform.position;
        moverB.transform.rotation = Quaternion.identity;
        moverB.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void RespawnPiano()
    {
        transform.position = spawnpoint.transform.GetChild(2).transform.position;
        transform.rotation = Quaternion.identity;
        spriteRenderer.enabled = true;
        moverA.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        moverB.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }


    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
