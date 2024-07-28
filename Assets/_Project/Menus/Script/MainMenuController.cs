using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class MainMenuController : MonoBehaviour
{
    public string gameScene;
    public string settings;
    [Header("Audio")]
    public StudioEventEmitter clickSound;


    public void PlayGame()
    {
        //SceneManager.LoadScene(gameScene);
        //playButtonEffect.Play();
        clickSound.Play();
        Invoke(nameof(LoadGame), 0.2f);
        
    }

    private void LoadGame()
    {
        LoadingController.instance.LoadScene(3);
    }


    public void quitGame()
    {
        clickSound.Play();
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Settings()
    {
        clickSound.Play();
        SceneManager.LoadScene(settings);

    }
}

