using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPlayer : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    
    public DialogueUI DialogueUI => dialogueUI;
    
    public Iinteractable Interactable { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !DialogueUI.IsOpen)
        {
            if (Interactable != null)
            {
                Interactable.Interact(this);
            }
        }
    }
}
