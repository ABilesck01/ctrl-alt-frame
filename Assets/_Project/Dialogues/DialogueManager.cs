using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string nextScene;
    private Queue<string> sentences;

    public static DialogueManager instance;

    private bool isWriting;
    private string sentence;
    private Coroutine writeCoroutine;

    private void Awake()
    {
        instance = this;
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        //nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);

           
        }
        DisplayNextSentence();
       
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if(!isWriting)
        {
            sentence = sentences.Dequeue();
            writeCoroutine = StartCoroutine(WriteSentence(sentence));
        }
        else
        {
            if(writeCoroutine != null)
            {
                StopCoroutine(writeCoroutine);
            }
            dialogueText.text = sentence;
            isWriting = false;
        }
    }

    private IEnumerator WriteSentence(string sentence)
    {
        isWriting = true;
        dialogueText.text = "";

        foreach (char item in sentence.ToCharArray())
        {
            dialogueText.text += item;
            yield return null;
        }
        isWriting = false;
    }

    private void EndDialogue()
    {
        Debug.Log("End of conversation");   
        SceneManager.LoadScene(nextScene);
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            DisplayNextSentence();
        }
        if (Gamepad.current != null)
        {
            foreach (var button in Gamepad.current.allControls)
            {
                if (button is ButtonControl buttonControl && buttonControl.wasPressedThisFrame)
                {
                    DisplayNextSentence();
                    break;
                }
            }
        }
    }
}
