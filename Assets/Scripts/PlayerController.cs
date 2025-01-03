using UnityEngine;
using System.Collections; // Untuk coroutine

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 2.5f; // The force of the jump
    public float moveForce = 2f; // The force for moving in the direction
    public float groundCheckDistance = 0.1f; // The distance to check for the ground
    public LayerMask groundLayer; // Layer of the ground to check collision
    public float jumpCooldown = 0.5f; // Cooldown time for jumping

    private Rigidbody rb;
    private bool isGrounded;
    private bool canJump = false; // Initially false, will be set to true after 3 seconds
    private bool canMove = false; // Flag to control whether the frog can move
    private float lastJumpTime;
    private GameManager gameManager;

    private bool isFinished = false; // New flag to stop movement after finish

    void Start()
    {
        // Get the Rigidbody component attached to the object
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

        // Start the coroutine to delay the start of the game
        StartCoroutine(DelayGameStart()); 
    }

    // Coroutine to delay the game and player's movement for 3 seconds
    IEnumerator DelayGameStart()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        canJump = true; // Allow jumping after 3 seconds
        canMove = true; // Allow movement after 3 seconds
        Debug.Log("Lompat dan bergerak sudah bisa setelah 3 detik!");
    }

    void Update()
    {
        // If finished, prevent all movements
        if (isFinished) return;

        // If the player can't move, don't process any input
        if (!canMove) return;

        // Check if the object is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Allow jumping only when grounded and cooldown has passed, and after 3 seconds
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump(Vector3.up); // Default vertical jump
            StartCooldown();
        }

        // Handle directional jumps when in the air (not grounded)
        if (!isGrounded && canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Forward
            {
                Jump(Vector3.forward);
                StartCooldown();
            }
            else if (Input.GetKeyDown(KeyCode.S)) // Backward
            {
                Jump(Vector3.back);
                StartCooldown();
            }
            else if (Input.GetKeyDown(KeyCode.A)) // Left (Sideways)
            {
                Jump(Vector3.left);
                StartCooldown();
            }
            else if (Input.GetKeyDown(KeyCode.D)) // Right (Sideways)
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
        if (collision.gameObject.CompareTag("FinishLine")) // Periksa apakah katak menyentuh finish line
        {
            isFinished = true; // Hentikan semua pergerakan
            rb.velocity = Vector3.zero; // Hentikan gerakan fisika
            rb.angularVelocity = Vector3.zero; // Hentikan rotasi fisika
            Debug.Log("Katak 1 telah mencapai garis finish!");
            
            if (gameManager != null)
            {
                gameManager.FrogFinished(); // Laporkan ke GameManager
            }
        }
    }
}
