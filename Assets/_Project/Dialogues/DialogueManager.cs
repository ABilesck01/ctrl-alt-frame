using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;

    public void StartDialogue( Dialogue dialogue)
    {

        nameText.text = dialogue.name;

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
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;   
    }

    private void EndDialogue()
    {
        Debug.Log("End of conversation");
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

}
