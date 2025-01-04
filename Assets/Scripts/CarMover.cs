using UnityEngine;

public class CarMover : MonoBehaviour
{
    public float speed = 5f;
    public float boundaryX = 400f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.x >= boundaryX || transform.position.x <= -boundaryX)
        {
            Destroy(gameObject); // Hancurkan mobil
        }
    }
}