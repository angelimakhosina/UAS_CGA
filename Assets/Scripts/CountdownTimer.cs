using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 3f; // Mulai dari 3
    [SerializeField] TMP_Text countdownText; // Menggunakan TMP_Text

    void Start()
    {
        UpdateCountdownText();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0; // Pastikan tidak negatif
                countdownText.text = ""; // Hilangkan teks setelah selesai
            }
            else
            {
                UpdateCountdownText();
            }
        }
    }

    void UpdateCountdownText()
    {
        countdownText.text = Mathf.Ceil(currentTime).ToString(); // Tampilkan angka bulat
    }
}
