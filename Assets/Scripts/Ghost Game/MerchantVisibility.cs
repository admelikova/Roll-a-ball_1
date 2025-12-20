using UnityEngine;
using TMPro;

public class MerchantVisibility : MonoBehaviour, IInteractable {

    public TextMeshProUGUI interactText;
    public GameObject dialoguePanel;

    void Start() {
        
    }

    void FixedUpdate() {
        
    }

    public void Interact() {
        if (!dialoguePanel.activeSelf) {
            interactText.enabled = true;
        }
    }

}