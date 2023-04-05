using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUIScript : MonoBehaviour
{
    private VisualElement[] hearts;
    [SerializeField] private Sprite fullHeart, halfHeart, emptyHeart;

    private const string heartBarName = "HeartBar";
    private const string heartStyleClass = "health-heart";
    
    public void drawHearts(VisualElement root, PlayerController playerController) {
        VisualElement heartBar = root.Q(heartBarName);

        clearHearts(heartBar);

        // determine how many hearts to make total based off of max health
        float maxHealthRemainder = playerController.maxHealth % 2;
        int heartsToMake = (int)((playerController.maxHealth / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++) {
            // createFullHeart();
            heartBar.Add(createNewHeart());
        }

        // display hearts according to heart status
        int index = 0;
        foreach (VisualElement heart in heartBar.Children()) {
            int heartStatusRemainder = (int)Mathf.Clamp(playerController.currHealth - (index * 2), 0, 2);
            setHeartImage((HeartStatus)heartStatusRemainder, heart);
            index++;
        }
    }

    private VisualElement createNewHeart() {
        VisualElement heart = new VisualElement();
        heart.AddToClassList(heartStyleClass);

        return heart;
    }

    private void clearHearts(VisualElement heartBar) {
        heartBar.Clear();
    }

    public void setHeartImage(HeartStatus status, VisualElement heart) {
        switch (status)
        {
            case HeartStatus.Empty:
                heart.style.backgroundImage = new StyleBackground(emptyHeart);
                break;
            case HeartStatus.Half:
                heart.style.backgroundImage = new StyleBackground(halfHeart);
                break;
            case HeartStatus.Full:
                heart.style.backgroundImage = new StyleBackground(fullHeart);
                break;
        }
    }

    public enum HeartStatus {
        Empty = 0,
        Half = 1,
        Full = 2
    }
}
