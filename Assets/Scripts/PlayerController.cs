using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour {
    // rigidbody of the player
    private Rigidbody rb;

    // variable to keep track of collect "PickUp" objects
    private int count;

    // movement along X and Y axes
    private float movementX;
    private float movementY;

    // speed at which the player moves
    public float speed = 0;

    // UI text component to display count of "PickUp" objects collected
    public TextMeshProUGUI countText;

    // UI object to display winning text
    public GameObject winTextObject;

    // Start is called once before the first frame update
    void Start() {
        // get and store the rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();

        // initialize count to zero and update the count display
        count = 0;
        SetCountText();

        // initially set the win text to be inactive
        winTextObject.SetActive(false);
    }

    // this function is called when a move input is detected
    void OnMove(InputValue movementValue) {
        // convert the input value into a Vector2 for movement
        Vector2 movementVector = movementValue.Get<Vector2>();

        // store the X and Y components of the movement
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // this function is called once per fixed frame-rate frame
    private void FixedUpdate()
    {
        // create a 3D movement vector using the X and Y inputs
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // apple force to the rigidbody to move the player
        rb.AddForce(movement * speed);

        if (transform.position.z >= 18) {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }

        if (transform.position.z >= 55) {
            Destroy(GameObject.FindGameObjectWithTag("Enemy2"));
        }

    }

    void OnTriggerEnter(Collider other)
    {
        // check if the object the player collided with has the "PickUp" tag
        if (other.gameObject.CompareTag("PickUp"))
        {
            // deactivate the collided object (make it disappear)
            other.gameObject.SetActive(false);

            // increment the count of "PickUp" objects collected and update the count display
            count++;
            SetCountText();
        }

        if (other.gameObject.CompareTag("PickUp (L)"))
        {
            other.gameObject.SetActive(false);

            //System.Random pickUpLValue = new System.Random();
            //count += pickUpLValue.Next(2, 5);

            count += 2;
            SetCountText();
        }

        GameObject[] door = GameObject.FindGameObjectsWithTag("Door1");
        GameObject door2 = GameObject.FindGameObjectWithTag("Door2");
        GameObject[] door3 = GameObject.FindGameObjectsWithTag("Door3");

        if (count >= 12) {
            foreach (GameObject d in door) {
                d.SetActive(false);
            }
        }

        if (count >= 72) {
            foreach (GameObject d in door3) {
                d.SetActive(false);
                Debug.Log("hi");
            }
        }

        if (count >= 32)
        {
            door2.SetActive(false);
        }

        

    }

    // function to update the displayed count of "PickUp" objects collected
    void SetCountText() {
        // update the count text with the current count
        countText.text = "Count: " + count.ToString();

        //check if the count has reached or exceeded the win condition
        if (count >= 114) {
            // display the win text
            winTextObject.SetActive(true);

            // destroy the enemy GameObjects (platform 5)
            GameObject[] enemies4 = GameObject.FindGameObjectsWithTag("Enemy4");

            foreach (GameObject e in enemies4) {
                Destroy(e);    
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy2") || collision.gameObject.CompareTag("Enemy3") || collision.gameObject.CompareTag("Enemy4")) {
            // destroy the current object
            Destroy(gameObject);

            // update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

}