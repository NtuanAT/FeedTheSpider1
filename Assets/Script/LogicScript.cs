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
    public GameObject warningSignal;
    public AudioManager audioManager;

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
    public void Warning()
    {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
        {
            warningSignal.SetActive(false);
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

    public void NextLevel()
    {
        SceneManager.LoadScene(_nextSceneName);
    }
}
