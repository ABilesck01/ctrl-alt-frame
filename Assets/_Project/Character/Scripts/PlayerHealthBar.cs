using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthBarSlider;
    public TextMeshProUGUI txtMessage;
    [Header("miniigame")]
    public GameObject minigameInputHelper;

    public static PlayerHealthBar instance;
    private bool isWriting;

    private void Start()
    {
        instance = this;

        MiniGameController.instance.OnStartMinimage.AddListener(_ =>
        {
            minigameInputHelper.SetActive(true);
        });

        MiniGameController.instance.OnCorrectMinigame.AddListener(() =>
        {
            minigameInputHelper.SetActive(false);
        });

        MiniGameController.instance.OnWrongMinigame.AddListener(() =>
        {
            minigameInputHelper.SetActive(false);
        });
    }

    public void SetHealth(int health)
    {
        healthBarSlider.value = health;
    }   

     public void SetMaxHealth(int health)
    {
        healthBarSlider.maxValue= health;
        healthBarSlider.value = health;

    }

    public void ShowText(string text)
    {
        if (isWriting)
            return;

        isWriting = true;
        StartCoroutine(WriteText(text));
        StartCoroutine(EraseText());
    }


    IEnumerator WriteText(string text)
    {
        txtMessage.text = "";
        foreach (var item in text.ToCharArray())
        {
            txtMessage.text += item;
            yield return null;
        }
    }

    IEnumerator EraseText()
    {
        yield return new WaitForSeconds(3f);
        txtMessage.text = "";
        isWriting = false;
    }
        
    
}
