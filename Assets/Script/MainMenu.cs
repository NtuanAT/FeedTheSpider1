using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator levelLoader;
	//Routine Load Level
	IEnumerator LoadLevel(string sceneName)
	{
		levelLoader.SetTrigger("Start");
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(sceneName);
	}
	public void PlayGame()
    {
        StartCoroutine(LoadLevel("Level1"));
    }

    public void ExitGame()
    {
        Application.Quit();
        //Debug.Log("Quit");
    }
}
