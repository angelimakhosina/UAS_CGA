using UnityEngine;

public class CarMover : MonoBehaviour
{
    public float speed = 5f;
    public float destroyTime = 10f;

    private Rigidbody rb;

    void Start()
    {
        // Add Rigidbody if not already present
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false; // Disable gravity for cars
            rb.isKinematic = true; // Avoid interference with physics forces
        }

        Destroy(gameObject, destroyTime); // Destroy after a set time
    }

    void FixedUpdate()
    {
        // Move the car forward using Rigidbody.MovePosition
        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Car collided with: " + collision.gameObject.name);
    }
}