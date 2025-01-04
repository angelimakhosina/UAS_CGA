using System.Collections;
using UnityEngine;
using TMPro; // Untuk TMP_Text

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
        if (!isRunning)
        {
            Debug.Log("Timer sudah berhenti sebelumnya.");
            return; // Timer sudah berhenti, tidak perlu melakukan apa-apa
        }

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

        Debug.Log($"Timer dihentikan. Waktu akhir: {timer:F2} detik");
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