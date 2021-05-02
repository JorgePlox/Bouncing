using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode
{
    none,
    infinite,
    levels
}
public class LevelManager : MonoBehaviour
{
    public static LevelManager sharedInstance;
    public GameMode currentGameMode;


    private void Awake()
    {
        sharedInstance = this;
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetGameModeNone() 
    {
        currentGameMode = GameMode.none;
    }

    public void SetGameModeLevels()
    {
        currentGameMode = GameMode.levels;
    }

    public void SetGameModeInfinite()
    {
        currentGameMode = GameMode.infinite;
    }
}
