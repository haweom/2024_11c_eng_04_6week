using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPlayer : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    
    public DialogueUI DialogueUI => dialogueUI;
    private PlayerMovement _playerMovement;
    
    public Iinteractable Interactable { get; set; }

    private void Awake()
    {
        _playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            if (Input.GetKeyDown(KeyCode.R) && !DialogueUI.IsOpen)
            {
                if (Interactable != null)
                {
                    Interactable.Interact(this);
                    _playerMovement.SetIdle();
                }
            }
        }
    }
}
