using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public Rigidbody player;
    public CoinSpawner coinSpawn;
    public PlayerStats pStats;
    internal float distanceCoin;
    public TextMeshProUGUI interactText;
    public GameObject dialoguePanel;

    private float minVal = Int32.MaxValue;
    private GameObject minCoins;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    void FixedUpdate() {
        //distanceCoin = coinSpawn.distances.Min();
        //distanceCoin = coinSpawn.spawnedCoinsAndDistances.Values.Min();

        if (player != null) {
            foreach (var item in coinSpawn.spawnedCoinsAndDistances){
                if (item.Value < minVal)
                {
                    minVal = item.Value;
                    minCoins = item.Key;
                }
            }

            distanceCoin = minVal;
        }
        //coinPiles.Add(GameObject.FindGameObjectWithTag("Coins"));

    }



// ---------------------------------- INPUT SYSTEM FUNCTIONS ------------------------------------------------------------------------------------------------------------------

    // function to collect coins
    public void CollectCoins(InputAction.CallbackContext context) {
        if (true && interactText.enabled) {
            if (true/*distanceCoin <= 4*/) {
            // increase money by 5
            pStats.money += 5;

            coinSpawn.spawnedCoinsAndDistances.Remove(minCoins);
            Destroy(minCoins);
            }
        }
    }

    public void TalkToMerchant(InputAction.CallbackContext context) {
        if (true) {
            dialoguePanel.SetActive(true);
        }
    }





}
