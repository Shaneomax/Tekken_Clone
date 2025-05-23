using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this line to fix the error

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        // You can press "Esc" to pause/resume the game.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game time
        pauseMenuUI.SetActive(true); // Show the pause menu
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true; // Make cursor visible
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game time
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to the center
        Cursor.visible = false; // Make cursor invisible
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Make sure time is running normally before loading the scene
        SceneManager.LoadScene("MainMenu"); // Ensure "MainMenu" is added to Build Settings
    }

    public void QuitGame()
    {
        Application.Quit(); // Properly call Quit
    }
}
