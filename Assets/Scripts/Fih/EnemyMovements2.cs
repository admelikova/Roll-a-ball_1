using UnityEngine;
using UnityEngine.AI;

public class EnemyMovements2 : MonoBehaviour {
    public Transform player;

    private NavMeshAgent navMeshAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update() {
        if (player != null && player.transform.position.z >= 25) {
            navMeshAgent.SetDestination(player.position);
        }
        
    }
}
