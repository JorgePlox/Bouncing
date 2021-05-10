using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public static AdManager sharedInstance;
    static int readyToShow = 0;

    private void Awake()
    {
        sharedInstance = this;
    }

    public void PlayAd()
    {

        if (Advertisement.IsReady("video") && readyToShow >= 2)
        {
            Advertisement.Show("video");
            readyToShow = 0;
        }

        readyToShow += 1;
    }

}
