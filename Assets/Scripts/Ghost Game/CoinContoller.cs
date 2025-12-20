using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CoinController : MonoBehaviour
{
/*
    public Rigidbody player;

    public CoinSpawner coinSpawn;
    public PlayerInteract playInteract;

    // reference to mesh renderer
    private Renderer c_Renderer;
    internal bool coinVisible = false;

    public TextMeshProUGUI interactText;

    void Start()
    {
        c_Renderer = GetComponent<Renderer>();
        if (c_Renderer == null)
        {
            Debug.LogError("GameObject has no Renderer component attached!");
            // disable the script if there is no renderer attached to the merchant
            this.enabled = false;
        }

        interactText.enabled = false;

    }

    void FixedUpdate()
    {
        if (c_Renderer.isVisible)
        {
            coinVisible = true;
            //Debug.Log("Coin is visible on screen.");
        }
        else
        {
            coinVisible = false;
            //Debug.Log("Coin is off screen.");
        }

        if (player != null) {
            if (coinVisible && playInteract.distanceCoin <= 4) {
                interactText.enabled = true;
            }

            if (!coinVisible || playInteract.distanceCoin > 4) {
                //interactText.enabled = false;
            }
        }

    }*/
}