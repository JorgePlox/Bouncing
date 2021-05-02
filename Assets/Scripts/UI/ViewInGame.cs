using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public Text coinsLabel;
    public Text timeLabel;
    //public Text maxScoreLabel;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int currentObjects = GameManager.sharedInstance.collectedObjects;
            coinsLabel.text = currentObjects.ToString();
        }

        //if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        //{
        //    float timeElapsed = Time.time;
        //    this.timeLabel.text = "Time\n" + timeElapsed.ToString("f0");
        //}

        //todo esto necesita ser corregido, el tiempo lo tiene que llevar el player y resetearlo cuando muera


    }
}
