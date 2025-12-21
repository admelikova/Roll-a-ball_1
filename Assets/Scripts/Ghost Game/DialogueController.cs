using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI interactText;
    public GameObject yesButton;
    public GameObject noButton;
    public string[] lines1;
    public string[] lines2;
    public string[] lines3;
    public string[] lines4;
    public string[] lines5;
    public string[] lines6;
    public float textSpeed;
    private int index;

    internal int merchantInteraction = 1;

    private bool buyMap;

    public Interactor inter;
    public PlayerStats playStats;
    public Lvl2PlayerMovement playerMove;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        textComponent.text = string.Empty;
        StartDialogue();
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            if (inter.who == 1) {
                switch (merchantInteraction) {
                    case 1:
                        ReadLines(lines1);
                        break;
                    default:
                        ReadLines_Lines3(lines3);
                        break;
                }
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
            switch (merchantInteraction) {
                case 1:
                    StartCoroutine(TypeLine(lines1));
                    break;
                default:
                    StartCoroutine(TypeLine(lines3));
                    if (index == 0) {
                        yesButton.SetActive(true);
                        noButton.SetActive(true);
                    }
                    break;
            }
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
            if (inter.who == 1 && merchantInteraction == 1) {
                merchantInteraction++;
            }

            gameObject.SetActive(false);
            index = 0;
            textComponent.text = string.Empty;
        }
    }

    void NextLine_Lines3(string[] lines) {
        if (index < lines.Length - 1) {
            switch (index) {
                case 0:
                    if (buyMap) {
                        index++;
                    }
                    else {
                        index = 3;
                    }
                    break;
                //yes
                case 1:
                    if (playStats.money >= 50) {
                        playStats.BoughtMap();
                        playerMove.mapBought = true;
                        index = 2;
                    }
                    else {
                        index = 7;
                    }
                    break;
                case 2:
                    index = 4;
                    break;
                case 4:
                    index = 5;
                    break;
                //no
                case 3:
                    index = 6;
                    break;
                case 6:
                    index = 5;
                    break;
                case 5:
                    index = 8;
                    //ReadLines_Lines3(lines3);
                    //NextLine(lines3);
                    break;
            }

            textComponent.text = string.Empty;
            StartCoroutine(TypeLine(lines));
        }
        else {
            if (inter.who == 1 && merchantInteraction == 1) {
                merchantInteraction++;
            }

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

    void ReadLines_Lines3(string[] lines) {
        if (textComponent.text == lines[index] && index != 5) {
            NextLine_Lines3(lines);
        }
        else {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    public void BuyMap() {    
        buyMap = true;

        yesButton.SetActive(false);
        noButton.SetActive(false);
    }

    public void NoBuyMap() {
        buyMap = false;
        
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }
}
