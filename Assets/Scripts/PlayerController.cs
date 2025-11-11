using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using System.Security.Cryptography;
using UnityEngine.UIElements;
using System.Collections;

public class PlayerController : MonoBehaviour {
    // rigidbody of the player
    private Rigidbody rb;


    // variables to keep track of collected "PickUp" objects and of number of lives
    private int count;
    private int lives;


    // movement along X and Y axes
    private float movementX;
    private float movementY;


    // speed at which the player moves (is this even used?)
    public float speed = 0;


    // jump power, how high the player can jump (is this also even used?)
    public float jumpPower = 0.5f;


    // variables for dash mechanic
    public float dashingPower = 12.5f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 3f;
    private bool isDashing = false;
    private float lastDashTime;
    private Vector3 dashDirection;


    // UI text component to display count of "PickUp" objects collected and UI text component to display number of lives
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;


    // UI object to display winning text
    public GameObject winTextObject;


    // variables for groundchecking
    public Transform groundCheckPos;
    public Vector3 groundCheckSize = new Vector3(0.5f, 0.05f, 0.5f);
    public LayerMask groundLayer;


    // gameobject reference to the pause menu
    public GameObject pauseMenu;

    // flag variable for pause function
    public static bool isPaused = false;


    // variables for audio usage
    [SerializeField] private AudioClip bubble1;
    private AudioSource audioSource;

    // ---------------------------------- START FUNCTION -------------------------------------------------------------------------------------------------------------

    // Start is called once before the first frame update
    void Start() {
        // get and store the rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();

        // initialize count to zero and update the count display
        count = 0;
        SetCountText();

        //initialize lives to three and update the lives display
        lives = 3;
        SetLivesText();

        // initially set the win text to be inactive
        winTextObject.SetActive(false);

        //
        pauseMenu.SetActive(false);

        // audio
        audioSource = GetComponent<AudioSource>();
    }

    // ---------------------------------- ONMOVE FUNCTION (NOT USED) -------------------------------------------------------------------------------------------------------------

    /*  NOTE: this function is not being used because of changed input system behavior (send messages -> invoke unity events)
        // this function is called when a move input is detected
        void OnMove(InputValue movementValue)
        {
            // convert the input value into a Vector2 for movement
            Vector2 movementVector = movementValue.Get<Vector2>();

            // store the X and Y components of the movement
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    */

    // ---------------------------------- UPDATE FUNCTION -------------------------------------------------------------------------------------------------------------------

    // this function is called once per fixed frame-rate frame
    private void FixedUpdate() {
        // create a 3D movement vector using the X and Y inputs
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // apple force to the rigidbody to move the player
        rb.AddForce(movement * speed);

        // destroy enemy after leaving platform they're on
        if (transform.position.z >= 16) {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
        if (transform.position.z >= 55) {
            Destroy(GameObject.FindGameObjectWithTag("Enemy2"));
        }

        // respawn and update lives count
        if (transform.position.y <= -18) {
            Respawn();
        }

        // dash mechanic
        if (isDashing) {
            rb.AddForce(dashDirection * dashingPower); // * Time.deltaTime runs weird?
            Debug.Log("dashingggg");
        }

    }

    // ---------------------------------- PICKUP COLLECTING FUNCTION -------------------------------------------------------------------------------------------------------------------

    void OnTriggerEnter(Collider other) {
        // check if the object the player collided with has the "PickUp" tag
        if (other.gameObject.CompareTag("PickUp")) {
            // deactivate the collided object (make it disappear)
            other.gameObject.SetActive(false);

            // play sfx
            audioSource.clip = bubble1;
            audioSource.Play();

            // increment the count of "PickUp" objects collected and update the count display
            count++;
            SetCountText();
        }

        // check if the object the player collided with has the "PickUp (L)" tag
        if (other.gameObject.CompareTag("PickUp (L)")) {
            // deactivate the collided object
            other.gameObject.SetActive(false);

            // increment the count of "PickUp" objects collected and update the count display
            count += 2;
            SetCountText();
        }

        // find door gameobjects
        GameObject[] door = GameObject.FindGameObjectsWithTag("Door1");
        GameObject door2 = GameObject.FindGameObjectWithTag("Door2");
        GameObject[] door3 = GameObject.FindGameObjectsWithTag("Door3");

        // deactivate doors if enough points are collected
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
        if (count >= 32) {
            door2.SetActive(false);
        }

    }

    // ---------------------------------- TEXT UI UPDATE FUNCTIONS -------------------------------------------------------------------------------------------------------------------

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

    // function to update the displayed number of lives
    void SetLivesText() {
        // update the lives text with the current number of lives
        livesText.text = "Lives: " + lives.ToString();

        // check if the lives trigger the lose condition
        if (lives <= 0) {
            // destroy the current object
            Destroy(gameObject);

            // update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

        }
    }

    // ---------------------------------- ENEMY COLLISION FUNCTION -----------------------------------------------------------------------------------------------------------------

    // function to handle collisions with enemies
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy2") || collision.gameObject.CompareTag("Enemy3") || collision.gameObject.CompareTag("Enemy4")) {
            // respawn
            Respawn();

            /*
            // destroy the current object
            Destroy(gameObject);

            // update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            */
        }
    }

    // ---------------------------------- INPUT SYSTEM FUNCTIONS ------------------------------------------------------------------------------------------------------------------

    // jump function to work with spacebar key input, player jumps up
    public void Jump(InputAction.CallbackContext context) {
        // grounded condition check to prevent infinite jumping
        if (isGrounded() && !isPaused) {
            // if spacebar is pressed
            if (context.performed) {
                // player jump
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z);
            }
        }
    }

    // new move function (previous -> OnMove()), player moves with arrow keys and WASD
    public void Move(InputAction.CallbackContext context) {
        if (!isPaused) {
            // read inpute from arrow keys or WASD
            Vector2 movementVector = context.ReadValue<Vector2>();

            // store the X and Y components of the movement
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    //dash function with shift key input, player dashes
    public void Dash(InputAction.CallbackContext context) {
        if (!isPaused) {
            if (context.performed && !isDashing && (Time.time >= lastDashTime + dashCooldown)) {
                StartDash();
            }
        }
    }

    public void Pause(InputAction.CallbackContext context) {
        if (!isPaused) {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        // (isPaused == true)
        else {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }



    // ---------------------------------- JUMP HELPER FUNCTIONS -----------------------------------------------------------------------------------------------------------------

    // function to check if player is grounded (touching the ground), to prevent infinite jumping
    private bool isGrounded() {
        // Physics.BoxCast "casts" a box downward from the player to check for collisions with objects of noted layer (groundLayer)
        if (Physics.BoxCast(groundCheckPos.position, groundCheckSize, Vector3.down, Quaternion.identity, 0.4f, groundLayer)) {
            return true;
        }
        return false;
    }

    // helper function for groundchecking
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);
    }


    // ---------------------------------- DASH HELPER FUNCTIONS -----------------------------------------------------------------------------------------------------------------

    private void StartDash() {
        isDashing = true;
        lastDashTime = Time.time;

        // determine dash direction (based on current movement direction)
        dashDirection = transform.forward;
        Invoke("StopDash", dashDuration);
    }

    private void StopDash() {
        isDashing = false;
    }


    // ---------------------------------- RESPAWN FUNCTION -----------------------------------------------------------------------------------------------------------------

    public void Respawn() {
        if (transform.position.x >= -25 && transform.position.z <= 26) {
            transform.position = new Vector3(0, 0.345f, 0);
        }
        else if (transform.position.x >= -25 && transform.position.z <= 65) {
            transform.position = new Vector3(-14, 2.345f, 30);
        }
        else if (transform.position.x >= -35 && transform.position.z >= 65) {
            transform.position = new Vector3(-12.5f, 5.628f + 0.345f, 85);
        }
        else if (transform.position.x <= -35 && transform.position.z >= 65) {
            transform.position = new Vector3(-57, 10.27f + 0.345f, 86);
        }
        else if (transform.position.x <= -25 && transform.position.z <= 65) {
            transform.position = new Vector3(-60, 10.27f + 0.345f, 53);
        }
        else {
            transform.position = new Vector3(0, 0.345f, 0);
        }

        lives--;
        SetLivesText();
    }


}