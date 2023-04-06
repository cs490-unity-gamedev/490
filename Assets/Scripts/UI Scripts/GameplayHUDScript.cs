using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayHUDScript : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private HealthUIScript healthUIScript;
    [SerializeField] private PauseUIScript pauseUIScript;
    [SerializeField] private GameStateLogic gameStateLogic;
    private VisualElement root;
    private VisualElement pauseMenu;
    private VisualElement pauseStatusIcon;
    private Label playerScoreNum;

    private const string pauseMenuName = "PauseMenu";
    private const string pauseStatusIconName = "PauseStatusIcon";
    private const string playerScoreNumName = "ScoreNum";

    // every time player health changes, fire event
    // TODO: can this somehow be moved to HealthUIScript?
    private void OnEnable() {
        PlayerController.onPlayerDamaged += () => healthUIScript.drawHearts(root, playerController);
        GameStateLogic.onPauseStatusChange += () => pauseUIScript.togglePauseMenuAndIconDisplay(gameStateLogic.isPaused, pauseMenu, pauseStatusIcon);
        GameStateLogic.onScoreChange += () => updateDisplayedScore();

        // Buttons
        root.Q<Button>("Resume").clicked += () => gameStateLogic.resumeGame();
        root.Q<Button>("Restart").clicked += () => gameStateLogic.startGame();
        root.Q<Button>("StartMenu").clicked += () => gameStateLogic.loadStartMenu();
        root.Q<Button>("Quit").clicked += () => gameStateLogic.quitGame();
        root.Q<Button>("PauseGroup").clicked += () => gameStateLogic.handlePauseChange();
    }

    private void OnDisable() {
        PlayerController.onPlayerDamaged -= () => healthUIScript.drawHearts(root, playerController);
        GameStateLogic.onPauseStatusChange -= () => pauseUIScript.togglePauseMenuAndIconDisplay(gameStateLogic.isPaused, pauseMenu, pauseStatusIcon);
        GameStateLogic.onScoreChange -= () => updateDisplayedScore();

        // Buttons
        root.Q<Button>("Resume").clicked -= () => gameStateLogic.resumeGame();
        root.Q<Button>("Restart").clicked -= () => gameStateLogic.startGame();
        root.Q<Button>("StartMenu").clicked -= () => gameStateLogic.loadStartMenu();
        root.Q<Button>("Quit").clicked -= () => gameStateLogic.quitGame();
        root.Q<Button>("PauseGroup").clicked -= () => gameStateLogic.handlePauseChange();
    }

    private void Awake() {
        root = GetComponent<UIDocument>().rootVisualElement;
        pauseMenu = root.Q(pauseMenuName);
        pauseStatusIcon = root.Q(pauseStatusIconName);
        playerScoreNum = root.Q<Label>(playerScoreNumName);

        pauseMenu.style.display = DisplayStyle.None;
        healthUIScript.drawHearts(root, playerController);
        // initializeButtonLogic();
    }

    private void updateDisplayedScore() {
        playerScoreNum.text = gameStateLogic.playerScore.ToString();
    }
    
    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape)) {
    //         pauseUIScript.togglePauseMenuAndIconDisplay(gameStateLogic.isPaused, pauseMenu, pauseStatusIcon);
    //     }
    // }

    // private void initializeButtonLogic() {
    //     root.Q<Button>("Resume").clicked += () => gameStateLogic.resumeGame();
    //     root.Q<Button>("Restart").clicked += () => gameStateLogic.startGame();
    //     root.Q<Button>("StartMenu").clicked += () => gameStateLogic.loadStartMenu();
    //     root.Q<Button>("Quit").clicked += () => gameStateLogic.quitGame();
    // }
}
