using UnityEngine;
using UnityEngine.AI;

public class PetMovement : MonoBehaviour {
    public Transform player;

    private NavMeshAgent navMeshAgent;

    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        offset = transform.position - player.transform.position;   
    }

    // Update is called once per frame
    void Update() {
        if (player != null) {
            navMeshAgent.SetDestination(player.position + offset);
        }
        
    }
}
