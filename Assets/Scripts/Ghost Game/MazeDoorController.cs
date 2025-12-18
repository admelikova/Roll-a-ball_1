using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MazeDoorController : MonoBehaviour
{

    public Rigidbody player;

    // reference to mesh renderer
    private Renderer d_Renderer;
    private float distanceDoor;
    internal bool doorVisible = false;

    public TextMeshProUGUI interactText;

    void Start()
    {
        d_Renderer = GetComponent<Renderer>();
        if (d_Renderer == null)
        {
            Debug.LogError("GameObject has no Renderer component attached!");
            // disable the script if there is no renderer attached to the merchant
            this.enabled = false;
        }

        interactText.enabled = false;

    }

    void FixedUpdate()
    {
        if (d_Renderer.isVisible)
        {
            doorVisible = true;
            //Debug.Log("Maze is visible on screen.");
        }
        else
        {
            doorVisible = false;
            //Debug.Log("Maze is off screen.");
        }

        if (player != null) {
            distanceDoor = Vector3.Distance(transform.position, player.position);
            //Debug.Log(distanceDoor);

            if (doorVisible && distanceDoor <= 8.5) {
                interactText.enabled = true;
            }

            if (!doorVisible || distanceDoor > 8.5) {
                //interactText.enabled = false;
            }
        }
    }
}