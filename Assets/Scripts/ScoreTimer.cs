using System.Collections;
using UnityEngine;
using TMPro; // Untuk TMP_Text

public class ScoreTimer : MonoBehaviour
{
    public TMP_Text currentTimeText; // Untuk menampilkan waktu saat ini
    public TMP_Text bestTimeText;    // Untuk menampilkan waktu terbaik
    public TMP_Text livesLeftText;  // Untuk menampilkan jumlah nyawa tersisa

    private float timer = 0f;        // Waktu berjalan
    private bool isRunning = false; // Status timer
    private float bestTime = Mathf.Infinity; // Waktu terbaik
    public float startDelay = 3f;   // Waktu delay sebelum timer dimulai

    public GameManager gameManager; // Referensi ke GameManager

    void Start()
    {
        // Muat best time yang sebelumnya disimpan (jika ada)
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
            UpdateBestTimeUI();
        }

        // Perbarui jumlah nyawa pada UI
        UpdateLivesLeftUI();
    }

    void Update()
    {
        if (isRunning)
        {
            // Hitung waktu berjalan
            timer += Time.deltaTime;
            UpdateCurrentTimeUI();
        }

        // Perbarui nyawa secara real-time
        UpdateLivesLeftUI();
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

        // Periksa apakah skor saat ini lebih baik (lebih kecil) daripada best time
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

    private void UpdateLivesLeftUI()
    {
        if (gameManager != null)
        {
            // Ambil jumlah nyawa dari GameManager
            int currentLives = gameManager.GetCurrentLives();

            // Perbarui teks nyawa tersisa pada UI
            livesLeftText.text = $"Lives: {currentLives}";
        }
        else
        {
            Debug.LogError("GameManager belum dihubungkan di Inspector!");
        }
    }
}