using UnityEngine;
using TMPro;

public class MerchantVisibility : MonoBehaviour
{

    public Rigidbody player;

    // reference to mesh renderer
    private Renderer m_Renderer;
    private float distanceMerchant;
    internal bool merchantVisible = false;

    public TextMeshProUGUI interactText;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        if (m_Renderer == null)
        {
            Debug.LogError("GameObject has no Renderer component attached!");
            // disable the script if there is no renderer attached to the merchant
            this.enabled = false;
        }

        interactText.enabled = false;

    }

    void FixedUpdate()
    {
        if (m_Renderer.isVisible)
        {
            merchantVisible = true;
            //Debug.Log("Merchant is visible on screen.");
        }
        else
        {
            merchantVisible = false;
            //Debug.Log("Merchant is off screen.");
        }


        if (player != null) {
            distanceMerchant = Vector3.Distance(transform.position, player.position);

            if (merchantVisible && distanceMerchant <= 4) {
                //Debug.Log("hiii");
                interactText.enabled = true;
            }

            // this messes with detection for maze door
            if (!merchantVisible || distanceMerchant > 4) {
                interactText.enabled = false;
            }
        }

    }
}