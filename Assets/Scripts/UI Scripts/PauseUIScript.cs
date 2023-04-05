using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseUIScript : MonoBehaviour
{
    [SerializeField] private Sprite pause, play;

    // TODO: code seems repetitive
    public void togglePauseMenuAndIconDisplay(bool isPaused, VisualElement pauseMenu, VisualElement gameStatusIcon) {
        if (isPaused) {
            pauseMenu.style.display = DisplayStyle.None;
        } else {
            pauseMenu.style.display = DisplayStyle.Flex;
        }
        setGameStateIcon(isPaused, gameStatusIcon);
    }

    private void setGameStateIcon(bool isPaused, VisualElement gameStatusIcon) {
        if (isPaused) {
            gameStatusIcon.style.backgroundImage = new StyleBackground(pause);
        } else {
            gameStatusIcon.style.backgroundImage = new StyleBackground(play);
        }
    }
}
