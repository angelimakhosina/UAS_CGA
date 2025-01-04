using UnityEngine;

public class PlayerController1Hard : MonoBehaviour
{
    public float jumpForce = 2.5f; // The force of the jump
    public float moveForce = 2f; // The force for moving in the direction
    public float groundCheckDistance = 0.1f; // The distance to check for the ground
    public LayerMask groundLayer; // Layer of the ground to check collision
    public float jumpCooldown = 0.5f; // Cooldown time for jumping
    public float startDelay = 3f; // Delay time before player can move

    private Rigidbody rb;
    private bool isGrounded;
    private bool canJump = true; // Flag to control jump spamming
    private float lastJumpTime;
    private GameManagerHard gameManager;

    private bool isFinished = false; // New flag to stop movement after finish
    private float startTime; // Variable to track time since the start

    void Start()
    {
        // Get the Rigidbody component attached to the object
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManagerHard>();

        // Store the start time to track the 3-second delay
        startTime = Time.time;
    }

    void Update()
    {
        // If finished, prevent all movements
        if (isFinished) return;

        // Check if 3 seconds have passed since the start
        if (Time.time - startTime < startDelay) return; // Prevent movement during the delay

        // Check if the object is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Allow jumping only when grounded and cooldown has passed
        if (isGrounded && Input.GetKeyDown(KeyCode.Return) && canJump) // Use Enter key for Player 2's jump
        {
            Jump(Vector3.up); // Default vertical jump
            StartCooldown();
        }

        // Handle directional jumps when in the air (not grounded)
        if (!isGrounded && canJump)
        {
            if (Input.GetKeyDown(KeyCode.S)) // Forward
            {
                Jump(Vector3.forward);
                StartCooldown();
            }
            else if (Input.GetKeyDown(KeyCode.Space)) // Backward
            {
                Jump(Vector3.back);
                StartCooldown();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left
            {
                Jump(Vector3.left);
                StartCooldown();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) // Right
            {
                Jump(Vector3.right);
                StartCooldown();
            }
        }

        // Reset the jump ability after cooldown
        if (Time.time - lastJumpTime >= jumpCooldown)
        {
            canJump = true;
        }
    }

    void Jump(Vector3 direction)
    {
        // Apply an upward force combined with a directional force
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Jump upwards
        Vector3 adjustedHorizontalDirection = direction * moveForce;
        rb.AddForce(direction * moveForce, ForceMode.Impulse); // Apply directional movement
        canJump = false; // Disable jumping until cooldown is over
    }

    void StartCooldown()
    {
        lastJumpTime = Time.time; // Record the time of the last jump
        canJump = false; // Prevent further jumps
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinishLine1")) // Periksa apakah katak menyentuh finish line
        {
            isFinished = true; // Hentikan semua pergerakan
            rb.velocity = Vector3.zero; // Hentikan gerakan fisika
            rb.angularVelocity = Vector3.zero; // Hentikan rotasi fisika
            Debug.Log("Katak 2 telah mencapai garis finish!");

            if (gameManager != null)
            {
                gameManager.FrogFinished(); // Laporkan ke GameManager
            }
        }
    }
}