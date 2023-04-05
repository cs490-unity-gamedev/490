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
    private VisualElement gameStatusIcon;

    private const string pauseMenuName = "PauseMenu";
    private const string gameStatusIconName = "GameStatusIcon";

    // every time player health changes, fire event
    // TODO: can this somehow be moved to HealthUIScript?
    private void OnEnable() {
        PlayerController.onPlayerDamaged += () => healthUIScript.drawHearts(root, playerController);
    }

    private void OnDisable() {
        PlayerController.onPlayerDamaged -= () => healthUIScript.drawHearts(root, playerController);
    }

    private void Awake() {
        root = GetComponent<UIDocument>().rootVisualElement;
        pauseMenu = root.Q(pauseMenuName);
        gameStatusIcon = root.Q(gameStatusIconName);
        

        pauseMenu.style.display = DisplayStyle.None;
        healthUIScript.drawHearts(root, playerController);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseUIScript.togglePauseMenuAndIconDisplay(gameStateLogic.isPaused, pauseMenu, gameStatusIcon);
        }
    }
}
