using System.Collections;
using System.Collections.Generic;
using Manoeuvre;
using TMPro;
using UnityEngine;

public abstract class DialogueScript : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI dialogueText;
    [SerializeField] protected string[] lines;
    [SerializeField] AudioClip typeSound;
    AudioSource audioSource;
    protected int index = 0;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("next line");
            NextLine();
        }
    }

    protected void TypeLine()
    {
        audioSource.Play();
        dialogueText.text = lines[index];
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            TypeLine();
        }
        else
        {
            EndDialogue();
        }
    }
    public abstract void EndDialogue();
}
