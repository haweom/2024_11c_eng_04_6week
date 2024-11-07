using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, Iinteractable
{
    
    [SerializeField] private DialogueObject dialogueObject;
    
    private MrCrabsScript _crabScript;

    private void Start()
    {
        _crabScript = GetComponentInParent<MrCrabsScript>();
    }

    public void Interact(InteractPlayer player)
    {
        player.DialogueUI.showDialogue(dialogueObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out InteractPlayer interactPlayer))
        {
            _crabScript.IsTalking = true;
            interactPlayer.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out InteractPlayer interactPlayer))
        {
            if (interactPlayer.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                _crabScript.IsTalking = false;
                interactPlayer.Interactable = null;
            }
        }
    }
}
