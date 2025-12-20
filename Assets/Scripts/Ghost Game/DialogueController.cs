using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI interactText;
    public string[] lines1;
    public string[] lines2;
    public string[] lines3;
    public float textSpeed;
    private int index;

    public Interactor inter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (inter.who == 1) {
                ReadLines(lines1);
            }
            else if (inter.who == 2) {
                ReadLines(lines2);
            }
        }
    }

    void StartDialogue() {
        interactText.enabled = false;
        index = 0;
        if (inter.who == 1) {
            StartCoroutine(TypeLine(lines1));
        }
        else if (inter.who == 2) {
            StartCoroutine(TypeLine(lines2));
        }
    }

    IEnumerator TypeLine(string[] lines) {
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(string[] lines) {
        if (index < lines.Length - 1) {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine(lines));
        }
        else {
            gameObject.SetActive(false);
            index = 0;
            textComponent.text = string.Empty;
        }
    }

    void ReadLines(string[] lines) {
        if (textComponent.text == lines[index]) {
            NextLine(lines);
        }
        else {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

}
