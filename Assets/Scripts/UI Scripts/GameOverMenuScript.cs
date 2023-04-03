using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverMenuScript : MonoBehaviour
{
    [SerializeField] GameStateLogic gameStateLogic; // had to make an object with GameStateLogic script to use this

    private void Awake() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("Play").clicked += () => gameStateLogic.startGame();
        root.Q<Button>("StartMenu").clicked += () => gameStateLogic.loadStartMenu();
        root.Q<Button>("Quit").clicked += () => gameStateLogic.quitGame();
    }
}
