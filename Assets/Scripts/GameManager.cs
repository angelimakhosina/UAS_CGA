using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Untuk fungsi SceneManager
using TMPro; // Untuk TMP_Text

public class GameManager : MonoBehaviour
{
    private int frogsFinished = 0; // Counter untuk katak yang mencapai garis finish
    public int totalFrogs = 2; // Total jumlah katak

    public int maxLives = 20; // Jumlah nyawa awal
    private int currentLives; // Nyawa tersisa

    public ScoreTimer scoreTimer; // Referensi ke skrip ScoreTimer

    public TMP_Text gameOverText; // Referensi ke teks Game Over
    public TMP_Text gameWonText;  // Referensi ke teks Game Won
    public GameObject backToMenuButton; // Referensi ke tombol "Back to Menu"

    void Start()
    {
        currentLives = maxLives; // Inisialisasi nyawa
        StartCoroutine(StartGameWithDelay());
    }

    // Coroutine untuk menunda game selama 3 detik
    IEnumerator StartGameWithDelay()
    {
        Time.timeScale = 1;  // Pastikan waktu berjalan normal
        Debug.Log("Menunggu 3 detik sebelum game dimulai...");
        yield return new WaitForSeconds(3f); // Tunggu 3 detik
        scoreTimer.StartTimer();  // Mulai timer setelah delay
        Debug.Log("Game dimulai setelah 3 detik!");
    }

    public void FrogFinished()
    {
        frogsFinished++;

        // Cek apakah semua katak telah selesai
        if (frogsFinished >= totalFrogs)
        {
            GameWon();
        }
    }

    public void FrogDied()
    {
        currentLives--;

        Debug.Log($"Nyawa tersisa: {currentLives}");

        // Cek apakah nyawa habis
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! Tidak ada nyawa tersisa.");
        Time.timeScale = 0; // Pause game

        // Aktifkan UI Game Over
        if (gameOverText != null) gameOverText.gameObject.SetActive(true);
        if (backToMenuButton != null) backToMenuButton.SetActive(true);
    }

    private void GameWon()
    {
        Debug.Log("Memanggil GameWon(). Semua katak telah mencapai garis finish!");
        Time.timeScale = 0; // Pause game

        // Aktifkan UI Game Won
        if (gameWonText != null)
        {
            gameWonText.gameObject.SetActive(true); // Aktifkan teks "Game Won"
        }
        if (backToMenuButton != null)
        {
            backToMenuButton.SetActive(true); // Tampilkan tombol kembali
        }

        // Hentikan timer dan periksa best time
        if (scoreTimer != null)
        {
            scoreTimer.StopTimer(); // Stop dan simpan best time jika lebih baik
        }
        else
        {
            Debug.LogError("ScoreTimer belum dihubungkan di Inspector!");
        }
    }

    public void BackToMenu()
    {
        // Pindah ke scene "Game"
        SceneManager.LoadScene("Game");
    }

    public int GetCurrentLives()
    {
        return currentLives; // Bisa dipanggil untuk menampilkan nyawa di UI
    }
}