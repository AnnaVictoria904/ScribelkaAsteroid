using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesScript : MonoBehaviour
{
    public GameObject lives;
    public GameObject lifePrefab;
    private GameObject firstLife, secondLife, thirdLife;
    // Start is called before the first frame update
    void Start()
    {
        firstLife = Instantiate(lifePrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, lives.transform);
        secondLife = Instantiate(lifePrefab, new Vector2(transform.position.x + 1f, transform.position.y), Quaternion.identity, lives.transform);
        thirdLife = Instantiate(lifePrefab, new Vector2(transform.position.x + 2f, transform.position.y), Quaternion.identity, lives.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("LoseLife") == 2)
        {
            Destroy(thirdLife);
        }
        if (PlayerPrefs.GetInt("LoseLife") == 1)
        {
            Destroy(secondLife);
        }
        if (PlayerPrefs.GetInt("LoseLife") == 0)
        {
            Destroy(firstLife);
        }
    }
}
