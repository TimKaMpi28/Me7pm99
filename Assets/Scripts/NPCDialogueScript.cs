using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Manoeuvre;

public class NPCDialogueScript : DialogueScript
{
    [SerializeField] GameObject[] HUDObjects;
    ManoeuvreFPSController player;


    private void OnEnable()
    {
        index = 0;
        player = GameObject.FindWithTag("Player").GetComponent<ManoeuvreFPSController>();
        audioSource = GetComponent<AudioSource>();
        TypeLine();
        player.isInteracting = true;
        foreach (GameObject obj in HUDObjects)
        {
            obj.SetActive(false);
        }
    }

    public override void EndDialogue()
    {
        gameObject.SetActive(false);

        player.isInteracting = false;
        player.enabled = true;
        foreach (GameObject obj in HUDObjects)
        {
            obj.SetActive(true);
        }
        MMoController.instance.EndLevel();
    }

}
