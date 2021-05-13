using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    //Locked Levels
    //https://www.google.com/search?q=lock+and+unlock+level+unity&oq=unity+lock+and+unlock+le&aqs=chrome.1.69i57j0i22i30.5109j0j9&sourceid=chrome&ie=UTF-8#kpvalbx=_AmyPYPbRIOKq1sQP_JqsyAE15

    public Button[] levelButtons;
    private int fristLevel = 4;
    private int zoneCheckpoint = 8;
    private int nextSceneLoad;


    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", fristLevel);

        for (int i=0; i <levelButtons.Length; i++)
        { if (i + 4 > currentLevel)
            {
                levelButtons[i].interactable = false;
            } 
        }
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


    public void LoadNextScene(Collider2D otherCollider)
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;

        if (otherCollider.gameObject.tag == "Player")
        {
            ChangeScene("LevelMenu");

            //Ver si se desbloqueó un nivel 
            //El 12 es porque no quiero desbloquear nada después del 9 (9+4 == 13). Borrar && nextSceneLoad != 12 cuando saque el verdadero 9
            if (nextSceneLoad > PlayerPrefs.GetInt("CurrentLevel", fristLevel) && nextSceneLoad != 13)
            {
                PlayerPrefs.SetInt("CurrentLevel", nextSceneLoad);
            }
        }
    
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void restartZone()
    {
        if (PlayerPrefs.GetInt("CurrentLevel", fristLevel) >= zoneCheckpoint)
        {
            PlayerPrefs.SetInt("CurrentLevel", zoneCheckpoint);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", fristLevel);
        }
    }
}
