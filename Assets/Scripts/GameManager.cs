using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject sm;
    public GameObject smallAsteroids;
    public GameObject mediumAsteroids;
    public GameObject largeAsteroids;
    public GameObject eraseBest;
    public GameObject restartButton;
    public GameObject panel;
    public TextMeshProUGUI wave;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject bestScorePanel;
    private float sumTime, waitTime, asteroidSpawn;
    private float asteroidTime, maxSpawn, minSpawn;
    private int numWave, score;
    private int bestScore = 0;
    private bool increased;
    public TextMeshProUGUI scoreResult;
    public AudioClip smallExp;
    public AudioClip midExp;
    public AudioClip bigExp;
    public AudioClip waveUp;
    // Start is called before the first frame update
    void Start()
    {
        scoreResult.gameObject.SetActive(false);
        PlayerPrefs.SetFloat("Force", 80f);
        bestScore = PlayerPrefs.GetInt("BestScore");
        eraseBest.SetActive(false);
        restartButton.SetActive(false);
        PlayerPrefs.SetInt("SmallAst", 0);
        PlayerPrefs.SetInt("MidAst", 0);
        PlayerPrefs.SetInt("BigAst", 0);
        score = 0;
        scoreText.text = "Score: " + 0;
        maxSpawn = 2f; minSpawn = 3f;
        asteroidTime = Random.Range(minSpawn, maxSpawn);
        asteroidSpawn = 0f;
        waitTime = 0f;
        numWave = 1;
        sumTime = 0f;
        increased = false;
        sm = GameObject.FindGameObjectWithTag("SpawnManager");
        sm.GetComponent<SpawnManager>().SpawnAsteroid(smallAsteroids);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        if (PlayerPrefs.GetInt("GameFinish") == 0)
        {
            sumTime += Time.deltaTime;
            asteroidSpawn += Time.deltaTime;
            if (numWave == 1)
            {
                if (asteroidTime <= asteroidSpawn)
                {
                    sm.GetComponent<SpawnManager>().SpawnAsteroid(smallAsteroids);
                    asteroidSpawn = 0f;
                    asteroidTime = Random.Range(minSpawn, maxSpawn);
                }
            }
            else
            {
                if (asteroidTime <= asteroidSpawn)
                {
                    int randAst = Random.Range(0, 3);
                    if (randAst == 0)
                    {
                        sm.GetComponent<SpawnManager>().SpawnAsteroid(smallAsteroids);
                    }
                    else if (randAst == 1)
                    {
                        sm.GetComponent<SpawnManager>().SpawnAsteroid(mediumAsteroids);
                    }
                    else
                    {
                        sm.GetComponent<SpawnManager>().SpawnAsteroid(largeAsteroids);
                    }
                    asteroidSpawn = 0f;
                    asteroidTime = Random.Range(minSpawn, maxSpawn);
                }
            }
            if (sumTime >= 15f)
            {
                if (!increased)
                {
                    numWave++;
                    increased = true;
                    PlayerPrefs.SetFloat("Force", PlayerPrefs.GetFloat("Force") + 2f);
                    GetComponent<AudioSource>().PlayOneShot(waveUp);
                }
                wave.text = "Wave " + numWave;
                wave.gameObject.SetActive(true);
                waitTime += Time.deltaTime;
                if (waitTime >= 1f)
                {
                    wave.gameObject.SetActive(false);
                    waitTime = 0f;
                    sumTime = 0f;
                    increased = false;
                    minSpawn -= 0.2f;
                    maxSpawn -= 0.2f;
                }
            }
            if (PlayerPrefs.GetInt("SmallAst") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(smallExp);
                score += 10;
                PlayerPrefs.SetInt("SmallAst", 0);
            }
            if (PlayerPrefs.GetInt("MidAst") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(midExp);
                score += 50;
                PlayerPrefs.SetInt("MidAst", 0);
            }
            if (PlayerPrefs.GetInt("BigAst") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(bigExp);
                score += 100;
                PlayerPrefs.SetInt("BigAst", 0);
            }
        }
        else
        {
            wave.gameObject.SetActive(false);
            panel.SetActive(true);
            bestScorePanel.SetActive(true);
            eraseBest.SetActive(true);
            restartButton.SetActive(true);
            scoreResult.text = "Better luck next time!";
            if (score >= PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", score);
                scoreResult.text = "Good job!";
            }
            scoreResult.gameObject.SetActive(true);
            bestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
            return;
        }
    }
    public void EraseBestScore()
    {
        PlayerPrefs.SetInt("BestScore", 0);
        bestScoreText.text = "0";
        PlayerPrefs.Save();
    }
    public void RestartGame()
    {
        PlayerPrefs.SetInt("SmallAst", 0);
        PlayerPrefs.SetInt("MidAst", 0);
        PlayerPrefs.SetInt("BigAst", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
