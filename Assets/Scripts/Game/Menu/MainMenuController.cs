using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip hoverSound;
    private AudioSource backgroundMusicSource;
    private AudioSource hoverSoundSource;

    private void Awake()
    {
        // Initialize the AudioSource component for background music
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.playOnAwake = false;
        backgroundMusicSource.loop = true; // Set to loop
        backgroundMusicSource.clip = backgroundMusic;

        // Initialize the AudioSource component for hover sound
        hoverSoundSource = gameObject.AddComponent<AudioSource>();
        hoverSoundSource.playOnAwake = false;
        hoverSoundSource.clip = hoverSound;

        // Play the background music
        backgroundMusicSource.Play();
    }

    private void OnApplicationQuit()
    {
        // Stop the background music when the application quits
        if (backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowLeaderboards()
    {
        SceneManager.LoadScene("Leaderboards");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnPointerEnterMenuChoice()
    {
        // Play the hover sound when the pointer enters a menu choice
        if (hoverSound != null)
        {
            hoverSoundSource.PlayOneShot(hoverSound);
        }
    }
}
