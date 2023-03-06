using System;
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
    public GameObject pauseMenu;
    public AudioManager audioManager;
    private bool _isPause = false;

    public string[] sceneNames;
    private string _thisSceneName, _nextSceneName; 
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        _thisSceneName = SceneManager.GetActiveScene().name;
        int i = Array.FindIndex<string>(sceneNames, s => s == _thisSceneName);
        if(i < sceneNames.Length - 1)
        {
            _nextSceneName = sceneNames[i+1];
        }
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_isPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
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
        audioManager.Stop(audioManager.theme);
        audioManager.Play("GameOver");
        gameOverScreen.SetActive(true);
    }
    public void WinGame()
    {
        audioManager.Stop(audioManager.theme);
        audioManager.Play("WinGame");
        gameWinScreen.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(_thisSceneName);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(_nextSceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        _isPause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        _isPause = false;
    }
}
