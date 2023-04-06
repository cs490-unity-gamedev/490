using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialScene4Script : MonoBehaviour
{
    [SerializeField] private GameStateLogic gameStateLogic;

    private VisualElement root;

    private void OnEnable() {
        root.Q<Button>("Play").clicked += () => gameStateLogic.startGame();
        root.Q<Button>("RestartTutorial").clicked += () => gameStateLogic.loadTutorial();
        root.Q<Button>("StartMenu").clicked += () => gameStateLogic.loadStartMenu();
    }

    private void OnDisable() {
        root.Q<Button>("Play").clicked -= () => gameStateLogic.startGame();
        root.Q<Button>("RestartTutorial").clicked -= () => gameStateLogic.loadTutorial();
        root.Q<Button>("StartMenu").clicked -= () => gameStateLogic.loadStartMenu();
    }

    private void Awake() {
        root = GetComponent<UIDocument>().rootVisualElement;
    }
}
