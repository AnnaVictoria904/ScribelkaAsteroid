using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private GameObject sm;

    public GameObject smallAsteroidsPrefab;
    public GameObject mediumAsteroidsPrefab;
    public GameObject largeAsteroidsPrefab;
    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SpawnManager");
        Vector3 directionToCenter = Vector3.zero - new Vector3(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f), transform.position.z);

        Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, directionToCenter);
        transform.rotation = newRotation;
        //transform.eulerAngles = new Vector3 (0, 0, newRotation.z + Random.value);

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * PlayerPrefs.GetFloat("Force"));
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("GameFinish") == 1)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (gameObject.CompareTag("SmallAst"))
            {
                PlayerPrefs.SetInt("SmallAst", 1);
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("MidAst"))
            {
                PlayerPrefs.SetInt("MidAst", 1);
                sm.GetComponent<SpawnManager>().SpawnSmallerAsteroid(smallAsteroidsPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z));
                sm.GetComponent<SpawnManager>().SpawnSmallerAsteroid(smallAsteroidsPrefab, new Vector3(transform.position.x, transform.position.y - Random.value, transform.position.z));
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("BigAst"))
            {
                PlayerPrefs.SetInt("BigAst", 1);
                sm.GetComponent<SpawnManager>().SpawnSmallerAsteroid(mediumAsteroidsPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z));
                sm.GetComponent<SpawnManager>().SpawnSmallerAsteroid(mediumAsteroidsPrefab, new Vector3(transform.position.x, transform.position.y - Random.value, transform.position.z));
                Destroy(gameObject);
            }
        }
    }
    public void SetAsteroidSize(GameObject asteroidPrefab)
    {
        if (asteroidPrefab.CompareTag("BigAst"))
        {
            gameObject.tag = "BigAst";
        }
        else if (asteroidPrefab.CompareTag("MidAst"))
        {
            gameObject.tag = "MidAst";
        }
        else if (asteroidPrefab.CompareTag("SmallAst"))
        {
            gameObject.tag = "SmallAst";
        }
    }
}
