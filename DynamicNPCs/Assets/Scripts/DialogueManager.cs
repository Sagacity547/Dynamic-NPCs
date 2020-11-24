﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentances;

    // Start is called before the first frame update
    void Start()
    {
        sentances = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        Debug.Log("Starting Converstation");
        nameText.text = dialogue.name;
        sentances.Clear();

        foreach (string sentance in dialogue.sentances)
        {
            sentances.Enqueue(sentance);
        }

        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        if (sentances.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentance = sentances.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentance(sentance));
    }

    IEnumerator TypeSentance (string sentance)
    {
        dialogueText.text = "";
        foreach(char letter in sentance.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("End of Conversation");
    }
}
