using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject asteroidPrefab;
    public GameObject asteroidParent;
    void Start()
    {
        //SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnAsteroid(GameObject asteroid)
    {
        float posX = gameObject.transform.localScale.x;
        float posY = gameObject.transform.localScale.y;

        Vector2 randPos = Random.onUnitSphere * gameObject.transform.localScale.x;

        while ((Vector2.Distance(randPos, gameObject.transform.position) < posX || Vector2.Distance(randPos, gameObject.transform.position) < posY) ||
                (Vector2.Distance(randPos, gameObject.transform.position) < -posX || Vector2.Distance(randPos, gameObject.transform.position) < -posY))
        {
            randPos = Random.onUnitSphere * gameObject.transform.localScale.x;
        }
        
        GameObject newAsteroid = Instantiate(asteroid, randPos, Quaternion.identity, asteroidParent.transform);

        // Configurar el tamaño del nuevo asteroide
        AsteroidScript asteroidScript = newAsteroid.GetComponent<AsteroidScript>();
        if (asteroidScript != null)
        {
            asteroidScript.SetAsteroidSize(asteroid);
        }
    }
    public void SpawnSmallerAsteroid(GameObject asteroidPrefab, Vector3 position)
    {
        // Utiliza la posición proporcionada en lugar de generar una nueva posición aleatoria
        GameObject newAsteroid = Instantiate(asteroidPrefab, position, Quaternion.identity, asteroidParent.transform);

        // Configurar el tamaño del nuevo asteroide
        AsteroidScript asteroidScript = newAsteroid.GetComponent<AsteroidScript>();
        if (asteroidScript != null)
        {
            asteroidScript.SetAsteroidSize(asteroidPrefab);
        }
    }
}
