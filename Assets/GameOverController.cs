using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
	public GameObject GameOverUI;

	public static bool isDead = false;

	public void GameOver() {
		GameOverUI.SetActive(true);
	}

	public void Restart(){
		GameOverUI.SetActive(false);
        SceneManager.LoadScene("Game");
	}

	public void MainMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void Quit() {
		Application.Quit();
	}
}
