using UnityEngine;

public class Lvl2CameraController : MonoBehaviour {

    // bg audio
    [SerializeField] private AudioClip bgMusic;
    private AudioSource audioSourceBG;

    // Start is called once before the first frame update
    void Start() {
        audioSourceBG = GetComponent<AudioSource>();

        audioSourceBG.clip = bgMusic;
        audioSourceBG.Play();
    }

}
