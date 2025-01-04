using System.Collections;
using UnityEngine;
using TMPro; // Untuk TMP_Text

public class ScoreTimerHard : MonoBehaviour
{
    public TMP_Text currentTimeTextHard; // Untuk menampilkan waktu saat ini
    public TMP_Text bestTimeTextHard;    // Untuk menampilkan waktu terbaik
    public TMP_Text livesLeftTextHard;   // Untuk menampilkan jumlah nyawa tersisa

    private float timer = 0f;        // Waktu berjalan
    private bool isRunning = false; // Status timer
    private float bestTimeHard = Mathf.Infinity; // Waktu terbaik, dimulai dari nilai besar
    public float startDelay = 3f;   // Waktu delay sebelum timer dimulai

    public GameManagerHard gameManagerHard; // Referensi ke GameManager

    void Start()
    {
        // Muat best time yang sebelumnya disimpan (jika ada)
        if (PlayerPrefs.HasKey("BestTimeHard"))
        {
            bestTimeHard = PlayerPrefs.GetFloat("BestTimeHard");
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

        // Periksa apakah skor saat ini lebih baik (lebih pendek) daripada best time
        if (timer < bestTimeHard)
        {
            bestTimeHard = timer;

            // Simpan waktu terbaik ke PlayerPrefs
            PlayerPrefs.SetFloat("BestTimeHard", bestTimeHard);
            PlayerPrefs.Save();

            // Perbarui UI waktu terbaik
            UpdateBestTimeUI();
        }

        Debug.Log($"Timer dihentikan. Waktu akhir: {timer:F2} detik");
    }

    private void UpdateCurrentTimeUI()
    {
        // Tampilkan waktu berjalan (format: detik dengan 2 desimal)
        currentTimeTextHard.text = $"Current Time: {timer:F2} s";
    }

    private void UpdateBestTimeUI()
    {
        // Tampilkan waktu terbaik (format: detik dengan 2 desimal)
        bestTimeTextHard.text = $"Best Time: {bestTimeHard:F2} s";
    }

    private void UpdateLivesLeftUI()
    {
        if (gameManagerHard != null)
        {
            // Ambil jumlah nyawa dari GameManager
            int currentLives = gameManagerHard.GetCurrentLives();

            // Perbarui teks nyawa tersisa pada UI
            livesLeftTextHard.text = $"Lives Left: {currentLives}";
        }
        else
        {
            Debug.LogError("GameManager belum dihubungkan di Inspector!");
        }
    }
}