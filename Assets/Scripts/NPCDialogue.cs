using System.Collections;
using System.Collections.Generic;
using Manoeuvre;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] GameObject startDialogueText;
    [SerializeField] GameObject dialogueObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startDialogueText.SetActive(true);
            ManoeuvreFPSController player = other.gameObject.GetComponent<ManoeuvreFPSController>();
            player.canInteract = true;
            player.dialogue = dialogueObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startDialogueText.SetActive(false);
            ManoeuvreFPSController player = other.gameObject.GetComponent<ManoeuvreFPSController>();
            player.canInteract = false;
            player.dialogue = null;
        }
    }
}
