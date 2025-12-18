using UnityEngine;

public class CameraController : MonoBehaviour {
    // reference to the player GameObject
    public GameObject player;

    // distance between the camera and the player
    private Vector3 offset;

    [SerializeField] private AudioClip bgMusic;

    private AudioSource audioSourceBG;


    // Start is called once before the first frame update
    void Start() {
        // calculate the initial offset between the camera's position and the player's position
        offset = transform.position - player.transform.position;

        audioSourceBG = GetComponent<AudioSource>();

        audioSourceBG.clip = bgMusic;
        audioSourceBG.Play();

    }

    // LateUpdate is called once per frame after all Update functions have been completed
    void LateUpdate() {
        // maintain the same offset between the camera and player throughout the game
        transform.position = player.transform.position + offset;

    }
}
