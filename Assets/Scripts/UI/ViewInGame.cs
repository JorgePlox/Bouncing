using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public Text coinsLabel;
    public Text timeLabel;
    public Text livesLabel;
    public Text maxScoreLabel;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (coinsLabel != null)
            {
                int currentObjects = GameManager.sharedInstance.collectedObjects;
                coinsLabel.text = currentObjects.ToString();
            }


            switch (LevelManager.sharedInstance.currentGameMode)
            {
                case GameMode.levels:

                    //Vidas
                    int currentLives = PlayerController.sharedInstance.GetLivesNumber();
                    livesLabel.text = currentLives.ToString();

                    //Tiempo
                    if (timeLabel != null)
                    timeLabel.text = "Time:\n" + GameManager.sharedInstance.remainingTime.ToString("f0");
                    break;

                case GameMode.infinite:
                    //tiempo
                    timeLabel.text = "Time:\n" + GameManager.sharedInstance.timeElapsed.ToString("f0");
                    maxScoreLabel.text = "Best:\n" + PlayerPrefs.GetFloat("BestTimeInfinite", 0).ToString("f0");
                    break;
            }
        }

        else if (GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            switch (LevelManager.sharedInstance.currentGameMode)
            {
                case GameMode.levels:
                    int currentLives = PlayerController.sharedInstance.GetLivesNumber();
                    livesLabel.text = currentLives.ToString();
                    break;
            }
        }

    }
}
