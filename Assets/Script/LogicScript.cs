using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerLife;
    public int playerScore;
    public Text txtScore;
    public Text txtLife;
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;
    public AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void Score(int point)
    {
        playerScore += point;
        txtScore.text = playerScore.ToString();
    }
    public void LoseLife(int health)
    {
        playerLife -= health;
        txtLife.text = playerLife.ToString();
    }
    public void GameOver()
    {
        audioManager.Stop("Theme");
        audioManager.Play("GameOver");
        gameOverScreen.SetActive(true);
    }
    public void WinGame()
    {
        audioManager.Stop("Theme");
        audioManager.Play("WinGame");
        gameWinScreen.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
