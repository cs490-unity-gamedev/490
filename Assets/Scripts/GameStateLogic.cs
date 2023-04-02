using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameStateLogic : MonoBehaviour
{
    private void Awake() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("Play").clicked += () => startGame();
        root.Q<Button>("Tutorial").clicked += () => loadTutorial();
        root.Q<Button>("Quit").clicked += () => quitGame();
    }
    private void startGame() {
        // TODO: currently have hard-coded 1
        SceneManager.LoadScene(1);
    }

    private void quitGame() {
        Application.Quit();
        Debug.Log("Player quit game.");
    }

    private void loadStartMenu() {
        // TODO: currently have hard-coded 0
        SceneManager.LoadScene(0);
    }

    private void loadTutorial() {
        SceneManager.LoadScene("TutorialScene1");
    }
}
