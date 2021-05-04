using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        LevelManager.sharedInstance.LoadNextScene(other);
    }
}
