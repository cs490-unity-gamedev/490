using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    [SerializeField]
    private int playerScore;
    [SerializeField]
    private Text scoreText;
    
    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd) {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }
}
