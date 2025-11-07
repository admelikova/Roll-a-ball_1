using UnityEngine;
using System.Collections; 

public class EnemyMovement3 : MonoBehaviour
{
    public Transform player;
    public Transform[] patrolPoints;
    public float speed = 6f;
    private int currentPointIndex;
    private bool isWaiting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        currentPointIndex = 0;
        transform.position = patrolPoints[currentPointIndex].position;
    }

    // Update is called once per frame
    void Update() {
        if (player.transform.position.x <= -30 && player.transform.position.y >= 10.2 && (player.transform.position.z >= 20 && player.transform.position.z <= 63)) {
            isWaiting = false;
        }
        if (!isWaiting) {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, patrolPoints[currentPointIndex].position) < 0.1f) {
                StartCoroutine(WaitAtWaypoint());
            }
        }
    }

    IEnumerator WaitAtWaypoint() {
            isWaiting = true;
            yield return new WaitForEndOfFrame(); // Wait for the specified time

            // Move to the next waypoint in the array, looping back to the start if needed
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;  // 1%2=1, 2%2=0 
            isWaiting = false;
        }

}
