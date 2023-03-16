using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour
{
    [SerializeField]
    private GameObject heartPrefab;
    // public float currHealth, maxHealth;
    [SerializeField]
    private PlayerController playerController;
    List<HealthHeart> hearts = new List<HealthHeart>();

    // every time player health changes, fire event
    private void OnEnable() {
        PlayerController.onPlayerDamaged += drawHearts;
    }

    private void OnDisable() {
        PlayerController.onPlayerDamaged -= drawHearts;
    }

    // Start is called before the first frame update
    private void Start()
    {
        drawHearts();
    }

    public void drawHearts() {
        clearHearts();

        // determine how many hearts to make total based off of max health
        float maxHealthRemainder = playerController.maxHealth % 2;
        int heartsToMake = (int)((playerController.maxHealth / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++) {
            createFullHeart();
        }

        // display hearts according to heart status
        for (int i = 0; i < hearts.Count; i++) {
            int heartStatusRemainder = (int)Mathf.Clamp(playerController.currHealth - (i * 2), 0, 2);
            hearts[i].setHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void createFullHeart() {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.setHeartImage(HeartStatus.Full);
        hearts.Add(heartComponent);
    }

    public void clearHearts() {
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }
}
