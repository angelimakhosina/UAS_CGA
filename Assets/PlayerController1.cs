using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float jumpForce = 2f; // The force of the jump
    public float moveForce = 2f; // The force for moving in the direction
    public float groundCheckDistance = 0.1f; // The distance to check for the ground
    public LayerMask groundLayer; // Layer of the ground to check collision
    public float jumpCooldown = 0.5f; // Cooldown time for jumping

    private Rigidbody rb;
    private bool isGrounded;
    private bool canJump = true; // Flag to control jump spamming
    private float lastJumpTime;

    void Start()
    {
        // Get the Rigidbody component attached to the object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
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
            if (Input.GetKeyDown(KeyCode.Q)) // Forward
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
        rb.AddForce(direction * moveForce, ForceMode.Impulse); // Apply directional movement
        canJump = false; // Disable jumping until cooldown is over
    }

    void StartCooldown()
    {
        lastJumpTime = Time.time; // Record the time of the last jump
        canJump = false; // Prevent further jumps
    }
}
