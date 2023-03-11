using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
	private bool _isPlaying = true;
	public int playerLife;
	public int playerScore;
	public Text txtScore;
	public Text txtLife;
	public GameObject gameOverScreen;
	public GameObject gameWinScreen;
	public GameObject pauseMenu;
	public AudioManager audioManager;
	private bool _isPause;

	public string[] sceneNames;
	private string _thisSceneName, _nextSceneName;
	private void Awake()
	{
		audioManager = FindObjectOfType<AudioManager>();
		_thisSceneName = SceneManager.GetActiveScene().name;
		int i = Array.FindIndex<string>(sceneNames, s => s == _thisSceneName);
		if (i < sceneNames.Length - 1)
		{
			_nextSceneName = sceneNames[i + 1];
		}
		Time.timeScale = 1.0f;	
		_isPause = false;
	}

	private void Update()
	{
		if(_isPlaying)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (_isPause)
				{
					ResumeGame();
				}
				else
				{
					PauseGame();
				}
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
		_isPlaying = false;
		gameOverScreen.SetActive(true);
	}
	public void WinGame()
	{
		audioManager.Stop(audioManager.theme);
		audioManager.Play("WinGame");
		_isPlaying = false;
		gameWinScreen.SetActive(true);
	}
	public void RestartGame()
	{
		SceneManager.LoadScene(_thisSceneName);
	}
	public void LoadMenu()
	{
		SceneManager.LoadScene("StartMenu");
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

	public void ExitGame()
	{
		Application.Quit();
	}
}
