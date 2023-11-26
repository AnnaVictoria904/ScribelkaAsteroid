using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject prefabBullet;
    public float speedThrusting = 1.0f;
    public float speedTurn = 1.0f;
    public float turnDirection = 0.0f;
    private bool thrusting = false;
    private bool played = false;
    public Rigidbody2D rb;
    private Vector3 playerPosition;
    private int lives;
    public AudioClip shoot;
    public AudioClip thrust;
    private float timeSound = 0f;
    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        PlayerPrefs.SetInt("LoseLife", lives);
    }

    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            PlayerPrefs.SetInt("GameFinish", 1);
        }
        if (PlayerPrefs.GetInt("GameFinish") == 0)
        {
            thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                turnDirection = 1.0f;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                turnDirection = -1.0f;
            }
            else
            {
                turnDirection = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Shot();
                GetComponent<AudioSource>().PlayOneShot(shoot);
            }
            playerPosition = transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            timeSound += Time.deltaTime;
            rb.AddForce(transform.up*speedThrusting);
            if (!played)
            {
                GetComponent<AudioSource>().PlayOneShot(thrust);
                played = true;
            }
            if (timeSound >= 0.25f)
            {
                played = false;
                timeSound = 0f;
            }
        }

        if (turnDirection != 0)
        {
            rb.AddTorque(turnDirection*speedTurn);    
        }
    }

    private void Shot()
    {
        GameObject o = Instantiate(prefabBullet, transform.position, transform.rotation, transform);
        Bullet b = o.GetComponent<Bullet>();
        b.Shot(transform.up);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BoundaryUp"))
        {
            this.transform.position = new Vector3(playerPosition.x, -playerPosition.y + 0.1f, 0);
        }
        if (collision.gameObject.CompareTag("BoundaryDown"))
        {
            this.transform.position = new Vector3(playerPosition.x, -playerPosition.y - 0.1f, 0);
        }
        if (collision.gameObject.CompareTag("BoundaryRight"))
        {
            this.transform.position = new Vector3(-playerPosition.x + 0.1f, playerPosition.y, 0);
        }
        if (collision.gameObject.CompareTag("BoundaryLeft"))
        {
            this.transform.position = new Vector3(-playerPosition.x - 0.1f, playerPosition.y, 0);
        }
        if (collision.gameObject.CompareTag("SmallAst") || collision.gameObject.CompareTag("MidAst") || collision.gameObject.CompareTag("BigAst"))
        {
            Destroy(collision.gameObject);
            Collider2D[] asteroidsAtZero = Physics2D.OverlapCircleAll(Vector2.zero, 0.64f);

            foreach (var asteroid in asteroidsAtZero)
            {
                if (asteroid.CompareTag("SmallAst") || asteroid.CompareTag("MidAst") || asteroid.CompareTag("BigAst"))
                {
                    Destroy(asteroid.gameObject);
                }
            }

            lives--;
            PlayerPrefs.SetInt("LoseLife", lives);
            transform.position = Vector2.zero;
        }
    }
}
