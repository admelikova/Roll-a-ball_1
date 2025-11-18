using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Security.Cryptography;
using UnityEngine.UIElements;
using System.Collections;

public class Lvl2PlayerMovement : MonoBehaviour {
    // rigidbody of the player
    private Rigidbody rb;

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


    // variables for groundchecking
    public Transform groundCheckPos;
    public Vector3 groundCheckSize = new Vector3(0.5f, 0.05f, 0.5f);
    public LayerMask groundLayer;

// ---------------------------------- START FUNCTION -------------------------------------------------------------------------------------------------------------

    // Start is called once before the first frame update
    void Start() {
        // get and store the rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();

    }

// ---------------------------------- UPDATE FUNCTION -------------------------------------------------------------------------------------------------------------------

    // this function is called once per fixed frame-rate frame
    private void FixedUpdate() {
        // create a 3D movement vector using the X and Y inputs
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // apple force to the rigidbody to move the player
        rb.AddForce(movement * speed);

        // dash mechanic
        if (isDashing) {
            rb.AddForce(dashDirection * dashingPower); // * Time.deltaTime runs weird?
            Debug.Log("dashingggg");
        }

    }

// ---------------------------------- ENEMY COLLISION FUNCTION -----------------------------------------------------------------------------------------------------------------

    // function to handle collisions with enemies
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy2") || collision.gameObject.CompareTag("Enemy3") || collision.gameObject.CompareTag("Enemy4")) {
            // destroy the current object
            Destroy(gameObject);
        }

    }

// ---------------------------------- INPUT SYSTEM FUNCTIONS ------------------------------------------------------------------------------------------------------------------

    // jump function to work with spacebar key input, player jumps up
    public void Jump(InputAction.CallbackContext context) {
        // grounded condition check to prevent infinite jumping
        if (isGrounded()) {
            // if spacebar is pressed
            if (context.performed) {
                // player jump
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z);
            }
        }
    }

    // new move function (previous -> OnMove()), player moves with arrow keys and WASD
    public void Move(InputAction.CallbackContext context) {
        if (true) {
            // read inpute from arrow keys or WASD
            Vector2 movementVector = context.ReadValue<Vector2>();

            // store the X and Y components of the movement
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    //dash function with shift key input, player dashes
    public void Dash(InputAction.CallbackContext context) {
        if (true) {
            if (context.performed && !isDashing && (Time.time >= lastDashTime + dashCooldown)) {
                StartDash();
            }
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


}