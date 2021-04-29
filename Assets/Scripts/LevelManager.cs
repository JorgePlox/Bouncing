using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    static LevelManager sharedInstance;


    private void Awake()
    {
        sharedInstance = this;
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
