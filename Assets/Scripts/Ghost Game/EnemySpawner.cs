using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Rigidbody player;
    public GameObject ghostEnemy;
    //public PlayerInteract playInteract;
    //public TextMeshProUGUI interactText;
    private Vector3 spawnPoint;
    private float timeBetweenSpawns = 60f;
    private GameObject spawnedGhost;
    private int ghostsSpawned = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player != null) {
            StartCoroutine(SpawnGhosts());
        }
    }

    IEnumerator SpawnGhosts()
    {   
        while (true) {
            yield return new WaitForSeconds(timeBetweenSpawns);

            spawnPoint = new Vector3(player.position.x, player.position.y, player.position.z - 20);
            
            if (ghostsSpawned < 5) {
                //Debug.Log("new ghost :o");

                // Instantiate a clone of the prefab at the specified position and rotation
                spawnedGhost = Instantiate(ghostEnemy, spawnPoint, Quaternion.identity);
                
                EnemyGhostMovement script; //creates that script data type
                script = spawnedGhost.GetComponent<EnemyGhostMovement>();
                script.player = player.transform;
                script.ghostsfx = Resources.Load<AudioClip>("Enemy_collide");
                script.enabled = true; 
                
                ghostsSpawned++;
            }

        }
    }
}
