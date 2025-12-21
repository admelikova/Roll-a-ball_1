using UnityEngine;
using UnityEngine.AI;

public class EnemyGhostMovement : MonoBehaviour {
    // reference to the player's transform
    public Transform player;

    // reference to the NavMeshAgent component for pathfinding
    private NavMeshAgent navMeshAgent;

    [SerializeField] internal AudioClip ghostsfx;
    private AudioSource audioSourceGhost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // get and store the NavMeshAgent component attached to this object
        navMeshAgent = GetComponent<NavMeshAgent>();

        //audio
        audioSourceGhost = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {
        // if there's a reference to the player...
        if (player != null) {
            // set the enemy's destination to the player's current location
            navMeshAgent.SetDestination(player.position);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            //sfx
            audioSourceGhost.clip = ghostsfx;
            audioSourceGhost.Play();
        }
    }

}
