using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class Lvl2PlayerMovement : MonoBehaviour {
    // rigidbody of the player
    private Rigidbody rb;

    // movement along X and Y axes
    private float movementX;
    private float movementY;

    // speed at which the player moves (is this even used?)
    public float speed = 0;

    // variables for dash mechanic
    public float dashingPower = 12.5f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 3f;
    private bool isDashing = false;
    private float lastDashTime;
    private Vector3 dashDirection;

    // variables for camera rotation handling
    [SerializeField]
    PlayerInput pi;
    [SerializeField]
    Transform vCam;
    [SerializeField] 
    private float upDownLookRange = 55f;
    private Vector2 lookInput;
    private float verticalRotation;
    private float horizontalRotation;
    InputAction lookAction;

    public SensitivityController sensitivityScript;


    // variables for pause panel
    public GameObject pausePanel;
    private bool paused = false;

    public PlayerStats playStats;

    public TextMeshProUGUI deathText;



// ---------------------------------- START FUNCTION -------------------------------------------------------------------------------------------------------------

    // Start is called once before the first frame update
    void Start() {
        // get and store the rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();

        lookAction = pi.currentActionMap.FindAction("Look");

        deathText.enabled = false;

        //UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        //UnityEngine.Cursor.visible = false;

    }

// ---------------------------------- UPDATE FUNCTION -------------------------------------------------------------------------------------------------------------------

    // this function is called once per fixed frame-rate frame
    private void FixedUpdate() {

        // get camera forward/right for movement
        Vector3 camForward = vCam.forward;
        Vector3 camRight = vCam.right;

        // flatten the camera vectors
        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // build movement relative to camera
        Vector3 moveDirection = camForward * movementY + camRight * movementX;

        if (rb != null) {
            // move the player
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);

            // dash mechanic
            if (isDashing) {
                rb.MovePosition(rb.position + dashDirection * dashingPower * Time.fixedDeltaTime);
            }

            //Look()
            ApplyHorizontalRotation(lookInput.x * sensitivityScript.sensitivity);
            ApplyVerticalRotation(lookInput.y * sensitivityScript.sensitivity);
        }
        
        /*
        if (pausePanel.activeSelf) {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }

        if (!pausePanel.activeSelf) {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }*/

/*      OLD -- used before adjusting movement to align with camera rotation
        // create a 3D movement vector using the X and Y inputs
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // move the player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // dash mechanic
        if (isDashing) {
            rb.MovePosition(rb.position + dashDirection * dashingPower * Time.fixedDeltaTime); 
            Debug.Log("dashingggg");
        }
*/

    }


// ---------------------------------- ENEMY COLLISION FUNCTIONS ------------------------------------------------------------------------------------------------------------------

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            // update health count and text
            playStats.health -= 10;
            playStats.SetHealthText();
            
            // freeze movement bc gameobject deletion deletes camera
            if (playStats.health <= 0) {
                deathText.enabled = true;
                Time.timeScale = 0f;
            }

        }

    }




// ---------------------------------- INPUT SYSTEM FUNCTIONS ------------------------------------------------------------------------------------------------------------------

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

    public void Pause(InputAction.CallbackContext context) {
        if (!paused) {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else {
            pausePanel.SetActive(false);    
            Time.timeScale = 1f;
        }
        paused = !paused;
    }

    public void Look(InputAction.CallbackContext context) {
        lookInput = context.ReadValue<Vector2>();
    }


// ---------------------------------- DASH HELPER FUNCTIONS -----------------------------------------------------------------------------------------------------------------

    private void StartDash() {
        isDashing = true;
        lastDashTime = Time.time;

        // determine dash direction (based on current movement direction)
        dashDirection = vCam.forward;
        dashDirection.y = 0f;
        dashDirection.Normalize();
        Invoke("StopDash", dashDuration);
    }

    private void StopDash() {
        isDashing = false;
    }

// ---------------------------------- LOOK HELPER FUNCTIONS -----------------------------------------------------------------------------------------------------------------

    private void ApplyHorizontalRotation(float rotationAmount) {
        //horizontalRotation += Mathf.Clamp(horizontalRotation - rotationAmount, -360f, 360f);
        horizontalRotation += rotationAmount;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotationAmount, 0));
    }

    private void ApplyVerticalRotation(float rotationAmount) {
        verticalRotation = Mathf.Clamp(verticalRotation - rotationAmount, -upDownLookRange, upDownLookRange);
        vCam.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }


}