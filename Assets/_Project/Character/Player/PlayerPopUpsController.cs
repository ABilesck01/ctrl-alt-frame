using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPopUpsController : MonoBehaviour
{
    [SerializeField] private GameObject popUpGameObject;
    [SerializeField] private TextMeshProUGUI txtPopUpBackground;
    [SerializeField] private TextMeshProUGUI txtPopUp;
    [SerializeField] private CanvasGroup canvasGroup;

    public static PlayerPopUpsController instance;

    private void Awake()
    {
        instance = this;
    }

    public void ShowPopUp(string text, Color forecolor, Color backgroundColor)
    {

        txtPopUp.text = text;
        txtPopUp.color = forecolor;
        txtPopUpBackground.text = text;
        txtPopUpBackground.color = backgroundColor;
        popUpGameObject.SetActive(true);
        txtPopUpBackground.characterSpacing = 0;
        StartCoroutine(StrechTextOverTime(txtPopUpBackground, 8, 19f));
        StartCoroutine(FadeInPopup(canvasGroup, 5));
        StartCoroutine(WaitAndFadeOut(canvasGroup, 2, 5));
    }

    private IEnumerator StrechTextOverTime(TextMeshProUGUI text, float duration, float strechAmount)
    {
        if (duration > 0)
        {
            text.characterSpacing = 0;
            float timer = 0;
            yield return null;

            while (timer < duration)
            {
                timer = timer + Time.deltaTime;
                text.characterSpacing = Mathf.Lerp(text.characterSpacing, strechAmount, duration * (Time.deltaTime / 20));
                yield return null;
            }
        }

    }

    private IEnumerator FadeInPopup(CanvasGroup canvas, float duration)
    {
        if (duration > 0)
        {
            canvas.alpha = 0;
            float timer = 0;
            yield return null;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration * Time.deltaTime);
                yield return null;

            }

            canvas.alpha = 1;
            yield return null;
        }
    }

    private IEnumerator WaitAndFadeOut(CanvasGroup canvas, float duration, float delay)
    {
        if (duration > 0)
        {
            while (delay > 0)
            {
                delay -= Time.deltaTime;
                yield return null;
            }

            canvas.alpha = 1;
            float timer = 0;
            yield return null;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * Time.deltaTime);
                yield return null;

            }

            canvas.alpha = 0;
            yield return null;
        }
    }
}
