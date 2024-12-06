using UnityEngine;

public class LogMover : MonoBehaviour
{
    public float speed = 3f;
    public bool moveLeft = true;

    void Update()
    {
        float direction = moveLeft ? -1 : 1;
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }
}