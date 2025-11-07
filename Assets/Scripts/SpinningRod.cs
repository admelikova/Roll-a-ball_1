using UnityEngine;

public class SpinningRod : MonoBehaviour {
    public Transform player;

    // Update is called once per frame
    void Update() {
        if (player.transform.position.z >= 70) {
            transform.Rotate(new Vector3(70, 0, 0) * Time.deltaTime);
        }
    }
}
