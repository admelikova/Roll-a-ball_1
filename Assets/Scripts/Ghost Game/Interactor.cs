using TMPro;
using UnityEngine;

interface IInteractable {
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    private float InteractRange = 4;
    internal int who;

    public TextMeshProUGUI interactText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactText.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange)) {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                if (hitInfo.collider.gameObject.CompareTag("Merchant")) {
                    who = 1;
                    Debug.Log("who = 1");
                }
                else {
                    Debug.Log("who = 2");
                    who = 2;
                }
                interactObj.Interact();
            }
            else {
                interactText.enabled = false;
            }
        }
    } 
}
