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
	public Animator levelLoader;
	public AudioManager audioManager;
	private bool _isPause;

	public string[] sceneNames;
	private string _thisSceneName, _nextSceneName;
	private void Awake()
	{
		_thisSceneName = SceneManager.GetActiveScene().name;
		audioManager = FindObjectOfType<AudioManager>();
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
				if (!_isPause)
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
	public void AddLife(int health)
	{
		playerLife += health;
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
	//Routine Load Level
	IEnumerator LoadLevel(string sceneName)
	{
		levelLoader.SetTrigger("Start");
		Time.timeScale = 1.0f;
		// Get the animation length
		float animationLength = levelLoader.GetCurrentAnimatorStateInfo(0).length;

		// Wait for the animation to finish playing
		yield return new WaitForSeconds(animationLength);
		SceneManager.LoadScene(sceneName);
	}
	public void RestartGame()
	{
		StartCoroutine(LoadLevel(_thisSceneName));
	}
	public void LoadMenu()
	{
		StartCoroutine(LoadLevel("StartMenu"));
	}
	public void NextLevel()
	{
		StartCoroutine(LoadLevel(_nextSceneName));
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
