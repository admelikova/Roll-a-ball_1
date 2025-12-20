using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public Rigidbody player;
    public GameObject coinpile;
    //public PlayerInteract playInteract;
    //public TextMeshProUGUI interactText;
    private Vector3 spawnPoint;
    private Vector3[] spawnLocations = new Vector3[] {new(632, 25.66738f, 680), new(640, 25.66738f, 640), new(673, 25.66738f, 656), 
                                                      new(667, 25.66738f, 542), new(755, 25.66738f, 542), new(742, 25.66738f, 691),
                                                      new(620, 25.66738f, 700), new(660, 25.66738f, 720), new(618, 25.66738f, 590)};
    private float timeBetweenSpawns = 10f;
    private GameObject spawnedPile;
    //internal List<GameObject> spawnedCoins = new List<GameObject>();
    //internal List<float> distances = new List<float>();
    //internal Dictionary<GameObject, float> spawnedCoinsAndDistances = new Dictionary<GameObject, float>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player != null) {
            StartCoroutine(SpawnCoins());
        }
    }

    IEnumerator SpawnCoins()
    {   
        while (true) {
            yield return new WaitForSeconds(timeBetweenSpawns);

            spawnPoint = new Vector3(Random.Range(585,750), 25.66738f, Random.Range(415,740));
            //spawnPoint = spawnLocations[Random.Range(0, spawnLocations.Length)];

            // Instantiate a clone of the prefab at the specified position and rotation
            spawnedPile = Instantiate(coinpile, spawnPoint, Quaternion.identity);

/*
            CoinController scriptOnInstance = spawnedPile.GetComponent<CoinController>();
            scriptOnInstance.player = player; // Assign the scene object
            scriptOnInstance.coinSpawn = this;
            scriptOnInstance.playInteract = playInteract;
            scriptOnInstance.interactText = interactText;
*/

            //Debug.Log("coins appeared :3");

            //spawnedCoins.Add(spawnedPile);
            //distances.Add(Vector3.Distance(spawnedPile.transform.position, player.transform.position));

            //spawnedCoinsAndDistances.Add(spawnedPile, Vector3.Distance(spawnedPile.transform.position, player.transform.position));

        }
    }


}
