using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayHUDScript : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private HealthUIScript healthUIScript;
    private VisualElement root;

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

        healthUIScript.drawHearts(root, playerController);
    }
}
