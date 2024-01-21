using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void ShowLeaderboards() {
        SceneManager.LoadScene("Leaderboards");
    }

    public void QuitGame() {
        Application.Quit();
    }
}