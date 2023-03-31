using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateLogic : MonoBehaviour
{
    public void startGame() {
        // TODO: currently have hard-coded 1
        SceneManager.LoadScene(1);
    }

    public void quitGame() {
        Application.Quit();
        Debug.Log("Player quit game.");
    }

    public void loadStartMenu() {
        // TODO: currently have hard-coded 0
        SceneManager.LoadScene(0);
    }

    public void loadTutorial() {
        SceneManager.LoadScene("TutorialScene1");
    }
}
