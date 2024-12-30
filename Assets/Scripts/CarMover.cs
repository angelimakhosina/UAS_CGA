using UnityEngine;

public class CarMover : MonoBehaviour
{
    public float speed = 5f;
    public float destroyTime = 10f;

    void Start(){
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}