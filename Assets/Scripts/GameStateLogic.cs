using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameStateLogic : MonoBehaviour
{
    // [SerializeField] GameplayHUDScript gameplayHUDScript;
    public bool isPaused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                resumeGame();
            } else {
                pauseGame();
            }
        }
    }

    public void startGame() {
        Time.timeScale = 1f;
        isPaused = false;
        // TODO: currently have hard-coded 1
        SceneManager.LoadScene(1);
    }

    public void quitGame() {
        Application.Quit();
        Debug.Log("Player quit game.");
    }

    public void loadStartMenu() {
        Time.timeScale = 1f;
        isPaused = false;
        // TODO: currently have hard-coded 0
        SceneManager.LoadScene(0);
    }

    public void loadTutorial() {
        SceneManager.LoadScene("TutorialScene1");
    }

    public void pauseGame() {
        // stop in-game clock (to pause game)
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resumeGame() {
        // resume in-game clock (to resume game)
        Time.timeScale = 1f;
        isPaused = false;
    }
}
