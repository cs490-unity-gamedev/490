using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Sprite pause, play;
    [SerializeField]
    private Image gameStatusIcon;
    [SerializeField]
    GameStateLogic gameStateLogic; // had to make an object with GameStateLogic script to use this
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            setGameStateIcon(isPaused);
            if (isPaused) {
                resumeGame();
            } else {
                pauseGame();
            }
        }
    }

    // TODO: should everything in here be categorized under GameStateLogic?

    public void pauseGame() {
        pauseMenu.SetActive(true);
        // stop in-game clock (to pause game)
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resumeGame() {
        pauseMenu.SetActive(false);
        // resume in-game clock (to resume game)
        Time.timeScale = 1f;
        isPaused = false;
    }
    // TODO: code is repetitive? bc start menu and quit are already in game state logic
    public void restartGame() {
        Time.timeScale = 1f;
        isPaused = false;
        // gameStateLogic.startGame(); // TODO: is it okay to just reload the scene to restart?
    }

    public void goToStartMenu() {
        Time.timeScale = 1f;
        isPaused = false;
        // gameStateLogic.loadStartMenu();
    }

    public void quitGame() {
        // gameStateLogic.quitGame();
    }

    private void setGameStateIcon(bool paused) {
        if (paused) {
            gameStatusIcon.sprite = pause;
        } else {
            gameStatusIcon.sprite = play;
        }
    }
}
