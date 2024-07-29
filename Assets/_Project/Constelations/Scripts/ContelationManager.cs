using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContelationManager : MonoBehaviour
{
    public ConstelationController constelationController_01;
    public ConstelationController constelationController_02;
    public ConstelationController constelationController_03;

    private int completeCount;

    private void Start()
    {
        constelationController_01.OnFinishConstelation.AddListener(CheckForEndGame);
        constelationController_02.OnFinishConstelation.AddListener(CheckForEndGame);
        constelationController_03.OnFinishConstelation.AddListener(CheckForEndGame);
    }

    private void CheckForEndGame(ConstelationController arg0)
    {
        if (!arg0.data.hasConstelation) 
            return;

        completeCount++;

        if(completeCount >= 3)
        {
            SceneManager.LoadScene("ending");
        }
    }
}
