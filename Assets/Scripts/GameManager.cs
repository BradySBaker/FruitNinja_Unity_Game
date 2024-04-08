using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool healthMode = true;

    public AudioSource audioS;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public GameObject menuUI;
    private static GameManager instance;
    public Button playButton;
    public TextMeshProUGUI highScoreText;

    public Food_Spawner junkSpawner;
    public Food_Spawner healthSpawner;

    public float avoidFoodMinWaitStart = 5;
    public float avoidFoodMaxWaitStart = 7;
    public float targetFoodMinWaitStart = 3;
    public float targetFoodMaxWaitStart = 5;

    public int scoreToIncreaseHealth = 10;
    private int scoreSinceLastIncrease = 0;
    public int maxHealth = 5;

    private int score = 0;
    public int health = 3;

    void Start()
    {
        Application.targetFrameRate = 90;
        health = 3;
        UpdateText();
        DisplayMenu();
    }

    private void DisplayMenu() {
        int highScore = PlayerPrefs.GetInt("highScore", 0);
        if (highScore < score) {
            PlayerPrefs.SetInt("highScore", score);
            highScoreText.text = "High Score " + score.ToString();
        } else {
            highScoreText.text = "High Score " + highScore.ToString();
        }
        Time.timeScale = 0f;
        menuUI.SetActive(true);
    }

    public void UpdateText() {
        scoreText.text = score.ToString();
        healthText.text = health.ToString();
    }
    public void StartGame(string mode) {

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Junk")) {
            Destroy(g);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("entity")) {
            Destroy(g);
        }
        Time.timeScale = 1;
        health = 3;
        score = 0;
        menuUI.SetActive(false);

        healthMode = mode == "Health Mode" ? true : false;

        junkSpawner.minWait = mode == "Health Mode" ? avoidFoodMinWaitStart : targetFoodMinWaitStart;
        junkSpawner.maxWait = mode == "Health Mode" ? avoidFoodMaxWaitStart : targetFoodMaxWaitStart;

        healthSpawner.minWait = mode == "Health Mode" ? targetFoodMinWaitStart : avoidFoodMinWaitStart;
        healthSpawner.maxWait = mode == "Health Mode" ? targetFoodMaxWaitStart : avoidFoodMaxWaitStart;

    }

    public void HandleFruitHit(string foodType, AudioClip sound) {
        audioS.clip = sound;
        audioS.pitch = Random.Range(.7f, 2);
        audioS.Play();

        if (healthMode && foodType != "Junk" || !healthMode && foodType == "Junk") {
            score++;
            scoreSinceLastIncrease++;
            if (scoreToIncreaseHealth == scoreSinceLastIncrease) {
                scoreSinceLastIncrease = 0;
                if (health <= maxHealth) {
                    health++;
                }
            }

        } else {
            health--;
            if (health < 1) {
                DisplayMenu();
            }
        }
        UpdateText();
    }

    public void DecreaseHealth(string foodType) {
        if (healthMode && foodType == "Junk" || !healthMode && foodType != "Junk") {
            return;
        }
        health--;
        healthText.text = health.ToString();
        if (health < 1) {
            DisplayMenu();
        }
    }

}
