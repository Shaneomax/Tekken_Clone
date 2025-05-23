using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public GameObject resultPanel;
    public Text resultText;
    public FightingController[] fightingController;
    public OpponentAI[] opponentAI;

    void Update()
    {
        foreach (FightingController controller in fightingController)
        {
            if (controller.gameObject.activeSelf && controller.currentHealth <= 0)
            {
                SetResult("You Lose");
                return;
            }
        }

        foreach (OpponentAI opponent in opponentAI)
        {
            if (opponent.gameObject.activeSelf && opponent.currentHealth <= 0)
            {
                SetResult("You Win!");
                return;
            }
        }
    }


    public void SetResult(string result)
    {
        resultText.text = result;
        resultPanel.SetActive(true);
        Time.timeScale = 0f; // Pauses the game
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene("MainMenu");
    }
}
