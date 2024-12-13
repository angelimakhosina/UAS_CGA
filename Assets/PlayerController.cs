using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 2f; // The force of the jump
    public float moveForce = 2f; // The force for moving in the direction
    public float groundCheckDistance = 0.1f; // The distance to check for the ground
    public LayerMask groundLayer; // Layer of the ground to check collision

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Get the Rigidbody component attached to the object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the object is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Jump when the space bar is pressed and the object is grounded
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump(Vector3.up); // Default vertical jump
        }

        // Handle directional jumps when in the air (not grounded)
        if (!isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W)) // Forward
            {
                Jump(Vector3.forward);
            }
            else if (Input.GetKeyDown(KeyCode.S)) // Backward
            {
                Jump(Vector3.back);
            }
            else if (Input.GetKeyDown(KeyCode.A)) // Left (Sideways)
            {
                Jump(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.D)) // Right (Sideways)
            {
                Jump(Vector3.right);
            }
        }
    }

    void Jump(Vector3 direction)
    {
        // Apply an upward force combined with a directional force
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Jump upwards
        rb.AddForce(direction * moveForce, ForceMode.Impulse); // Apply directional movement
    }
}
