using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        if (musicObjects.Length > 1)
        {
            for (int i = 0; i < musicObjects.Length; i++)
            {
                if (musicObjects[i] != this.gameObject)
                {
                    Destroy(musicObjects[i]);
                }
            }
        }

        DontDestroyOnLoad(this.gameObject);

    }

}
