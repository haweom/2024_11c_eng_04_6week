using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, Iinteractable
{
    
    [SerializeField] private DialogueObject dialogueObject;
    
    public void Interact(InteractPlayer player)
    {
        player.DialogueUI.showDialogue(dialogueObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out InteractPlayer interactPlayer))
        {
            interactPlayer.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out InteractPlayer interactPlayer))
        {
            if (interactPlayer.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                interactPlayer.Interactable = null;
            }
        }
    }
}
