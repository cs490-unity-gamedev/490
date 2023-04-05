using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseUIScript : MonoBehaviour
{
    [SerializeField] private Sprite pause, play;

    // TODO: code seems repetitive
    public void togglePauseMenuAndIconDisplay(bool isPaused, VisualElement pauseMenu, VisualElement pauseStatusIcon) {
        if (isPaused) {
            pauseMenu.style.display = DisplayStyle.Flex;
        } else {
            pauseMenu.style.display = DisplayStyle.None;
        }
        setGameStateIcon(isPaused, pauseStatusIcon);
    }

    private void setGameStateIcon(bool isPaused, VisualElement pauseStatusIcon) {
        if (isPaused) {
            pauseStatusIcon.style.backgroundImage = new StyleBackground(play);
        } else {
            pauseStatusIcon.style.backgroundImage = new StyleBackground(pause);
        }
    }
}
