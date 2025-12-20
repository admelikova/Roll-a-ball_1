using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public GameObject dialoguePanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    void FixedUpdate() {
        
    }



// ---------------------------------- INPUT SYSTEM FUNCTIONS ------------------------------------------------------------------------------------------------------------------

    public void TalkTo(InputAction.CallbackContext context) {
        if (true && interactText.enabled) {
            dialoguePanel.SetActive(true);
            interactText.enabled = false;
        }
    }

}
