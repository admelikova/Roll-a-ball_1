using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;
    //public TextMeshProUGUI interactText;

    internal int money = 0;
    internal int health = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMoneyText();
        SetHealthText();
        if (player != null) {
            StartCoroutine(IncreaseHealth());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
            StopAllCoroutines();
        }
    }

    public void SetMoneyText() {
        // update the money text with the current amount of money
        moneyText.text = "Money: $" + money.ToString();

    }
    
    public void SetHealthText() {
        healthText.text = "Health: " + health.ToString() + "%";
    }

    public void BoughtMap() {
        money -= 50;
        SetMoneyText();
    }

    IEnumerator IncreaseHealth() {
        while (true) {
            Debug.Log("upping health");
            yield return new WaitForSecondsRealtime(2.5f);
            if (health < 100) {
                health++;
                SetHealthText();
            }
        }    
    }

}
