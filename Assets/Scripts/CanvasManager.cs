using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject livesDisplay;
    public GameObject player;
    public TextMeshProUGUI bestScore;
    public GameObject bestScorePanel;
    public GameObject playButton;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("GameFinish", 0);
        bestScorePanel.SetActive(true);
        playButton.SetActive(true);
        bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        player.SetActive(false);
        startPanel.SetActive(true);
        livesDisplay.SetActive(false);
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameStart()
    {
        playButton.SetActive(false);
        bestScorePanel.SetActive(false);
        player.SetActive(true);
        startPanel.SetActive(false);
        livesDisplay.SetActive(true);
        Time.timeScale = 1.0f;
    }
}
