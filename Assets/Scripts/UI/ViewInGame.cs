using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public Text collectableLabel;
    //public Text scoreLabel;
    //public Text maxScoreLabel;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int currentObjects = GameManager.sharedInstance.collectedObjects;
            collectableLabel.text = currentObjects.ToString();
        }
    }
}
