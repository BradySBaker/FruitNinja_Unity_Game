using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    private int score = 0;
    private int health = 3;
    void Start()
    {
        UpdateText();
    }
    public void UpdateText() {
        scoreText.text = score.ToString();
        healthText.text = health.ToString();
    }

    public void AdjustPlayerHealthOrScore(string foodType) {
        Debug.Log(foodType);
        if (foodType != "Junk") {
            score++;
        } else {
            health--;
            if (health < 1) {
                SceneManager.LoadScene(0);
            }
        }
        UpdateText();
    }

}
