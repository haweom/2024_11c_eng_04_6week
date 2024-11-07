using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, Iinteractable
{
    
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private SpriteRenderer interactButton;
    
    private MrCrabsScript _crabScript;

    private void Start()
    {
        _crabScript = GetComponentInParent<MrCrabsScript>();
        interactButton.color = new Color(interactButton.color.r, interactButton.color.g, interactButton.color.b, 0f); 
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
            StartCoroutine(FadeIn());
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
                StartCoroutine(FadeOut());
                interactPlayer.Interactable = null;
            }
        }
    }
    
    private IEnumerator FadeIn()
    {
        float alpha = interactButton.color.a;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime;
            interactButton.color = new Color(interactButton.color.r, interactButton.color.g, interactButton.color.b, alpha);
            yield return null;
        }
        interactButton.color = new Color(interactButton.color.r, interactButton.color.g, interactButton.color.b, 1f);
    }

    private IEnumerator FadeOut()
    {
        float alpha = interactButton.color.a;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime;
            interactButton.color = new Color(interactButton.color.r, interactButton.color.g, interactButton.color.b, alpha);
            yield return null;
        }
        interactButton.color = new Color(interactButton.color.r, interactButton.color.g, interactButton.color.b, 0f);
    }
}
