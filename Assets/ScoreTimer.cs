using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTimer : MonoBehaviour
{
    public TMP_Text currentTimeText; // Untuk menampilkan waktu saat ini
    public TMP_Text bestTimeText;    // Untuk menampilkan waktu terbaik

    private float timer = 0f;        // Waktu berjalan
    private bool isRunning = false; // Status timer
    private float bestTime = Mathf.Infinity; // Waktu terbaik, dimulai dari nilai besar
    public float startDelay = 3f;   // Waktu delay sebelum timer dimulai

    void Start()
    {
        // Muat best time yang sebelumnya disimpan (jika ada)
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
            UpdateBestTimeUI();
        }

        // Mulai timer setelah delay
        StartCoroutine(StartTimerWithDelay());
    }

    IEnumerator StartTimerWithDelay()
    {
        // Tampilkan pesan atau animasi jika diperlukan saat delay
        currentTimeText.text = "Starting in 3 seconds...";
        yield return new WaitForSeconds(startDelay);

        // Mulai timer setelah delay
        StartTimer();
    }

    void Update()
    {
        if (isRunning)
        {
            // Hitung waktu berjalan
            timer += Time.deltaTime;
            UpdateCurrentTimeUI();
        }
    }

    public void StartTimer()
    {
        // Reset timer dan mulai hitung waktu
        timer = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;

        // Periksa apakah skor saat ini lebih baik (lebih pendek) daripada best time
        if (timer < bestTime)
        {
            bestTime = timer;

            // Simpan waktu terbaik ke PlayerPrefs
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();

            // Perbarui UI waktu terbaik
            UpdateBestTimeUI();
        }
    }

    private void UpdateCurrentTimeUI()
    {
        // Tampilkan waktu berjalan (format: detik dengan 2 desimal)
        currentTimeText.text = $"Current Time: {timer:F2} s";
    }

    private void UpdateBestTimeUI()
    {
        // Tampilkan waktu terbaik (format: detik dengan 2 desimal)
        bestTimeText.text = $"Best Time: {bestTime:F2} s";
    }
}
