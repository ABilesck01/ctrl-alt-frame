using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameScene;
    public string settings;
    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);

    }

    public void quitGame()
    {
        Debug.Log("Quit");
            Application.Quit();
    }

    public void Settings()
    {
        SceneManager.LoadScene(settings);

    }
}

