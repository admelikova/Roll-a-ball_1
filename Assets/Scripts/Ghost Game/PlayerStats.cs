using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMoneyText() {
        // update the money text with the current amount of money
        moneyText.text = "Money: $" + money.ToString();

    }
    
    public void SetHealthText() {
        healthText.text = "Health: " + health.ToString() + "%";
    }



}
