using UnityEngine;

public class DarkenCarColor : MonoBehaviour
{
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Ambil warna material asli
            Color originalColor = renderer.material.color;

            // Kurangi intensitas warna agar lebih gelap
            Color darkColor = originalColor * 0.5f;  // 0.5f untuk menggelapkan warna
            renderer.material.color = darkColor;
        }
    }
}
