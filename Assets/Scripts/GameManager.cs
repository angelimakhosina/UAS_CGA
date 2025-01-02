using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Untuk fungsi SceneManager
using TMPro; // Untuk TMP_Text

public class GameManager : MonoBehaviour
{
    private int frogsFinished = 0; // Counter for frogs that finished the game
    public int totalFrogs = 2; // Total number of frogs

    public int maxLives = 3; // Jumlah nyawa awal
    private int currentLives; // Nyawa tersisa

    public ScoreTimer scoreTimer; // Referensi ke skrip ScoreTimer

    public TMP_Text gameOverText; // Referensi ke teks Game Over
    public TMP_Text gameWonText;  // Referensi ke teks Game Won
    public GameObject backToMenuButton; // Referensi ke tombol "Back to Menu"

    void Start()
    {
        currentLives = maxLives; // Inisialisasi nyawa
        // Panggil coroutine untuk memulai game setelah 3 detik
        StartCoroutine(StartGameWithDelay());
    }

    // Coroutine untuk menunda game selama 3 detik
    IEnumerator StartGameWithDelay()
    {
        // Pastikan Time.timeScale 1 sebelum menunggu
        Time.timeScale = 1;  // Pastikan waktu berjalan normal

        Debug.Log("Menunggu 3 detik sebelum game dimulai...");
        yield return new WaitForSeconds(3f); // Tunggu 3 detik

        // Setelah 3 detik, game bisa dimulai
        scoreTimer.StartTimer();  // Mulai timer setelah delay
        Debug.Log("Game dimulai setelah 3 detik!");
    }

    public void FrogFinished()
    {
        frogsFinished++;

        // Check if all frogs have finished
        if (frogsFinished >= totalFrogs)
        {
            GameWon();
        }
    }

    public void FrogDied()
    {
        currentLives--;

        Debug.Log($"Nyawa tersisa: {currentLives}");

        // Check if lives are depleted
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! Tidak ada nyawa tersisa.");
        Time.timeScale = 0; // Pause the game
        scoreTimer.StopTimer(); // Hentikan timer saat game over

        if (gameOverText != null) gameOverText.gameObject.SetActive(true);
        if (backToMenuButton != null) backToMenuButton.SetActive(true);
    }

    private void GameWon()
    {
        Debug.Log("Semua katak telah mencapai garis finish! Game selesai.");
        Time.timeScale = 0; // Pause the game
        scoreTimer.StopTimer(); // Hentikan timer dan simpan skor
    
        if (gameWonText != null) gameWonText.gameObject.SetActive(true);
        if (backToMenuButton != null) backToMenuButton.SetActive(true);
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
