using TMPro;
using UnityEngine;

public class GhostActivatorScript : MonoBehaviour, IInteractable
{

    public TextMeshProUGUI interactText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact() {
        interactText.enabled = true;
        PetGhostMovement script; //creates that script data type
        script = GetComponent<PetGhostMovement>();
        script.enabled = true; 
    }

}
