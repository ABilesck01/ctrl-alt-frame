using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public static LoadingController instance;

    [SerializeField] private DOTweenAnimation loadingAnimation;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private float minLoadingTime = 1f;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        loadingAnimation.DOPlay();

        float elapsedTime = 0;


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            elapsedTime += Time.deltaTime;
            Debug.Log($"elapsed time loading - {elapsedTime}");
            yield return null;

            if(progress >= 0.9f)
            {
                while (elapsedTime < minLoadingTime)
                {
                    elapsedTime += Time.deltaTime;
                    Debug.Log($"elapsed time waiting - {elapsedTime}");
                    yield return null;
                }
                operation.allowSceneActivation = true;
            }

        }
    }


}
