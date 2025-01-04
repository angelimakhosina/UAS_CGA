using UnityEngine;
using UnityEngine.SceneManagement; // Tambahkan ini
using System.Collections; // Tambahkan untuk Coroutine

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;  // Panel yang berisi tombol Resume dan Back to Menu
    public GameObject pauseButton; // Tombol Pause yang ada di UI utama
    private bool canPause = false;  // Menandakan apakah tombol pause bisa diklik

    void Start()
    {
        // Pastikan panel pause disembunyikan saat game dimulai
        pausePanel.SetActive(false);
        pauseButton.SetActive(false); // Sembunyikan tombol pause pada awal permainan

        // Menunggu selama 3 detik sebelum tombol pause dapat diklik
        StartCoroutine(EnablePauseButtonAfterDelay(3f)); 
    }

    // Fungsi untuk mengaktifkan menu pause
    public void PauseGame()
    {
        if (canPause)  // Pastikan hanya bisa pause setelah 3 detik
        {
            Time.timeScale = 0f;  // Hentikan waktu (pause game)
            pausePanel.SetActive(true);  // Tampilkan panel pause
            pauseButton.SetActive(false); // Sembunyikan tombol pause
        }
    }

    // Fungsi untuk melanjutkan permainan
    public void ResumeGame()
    {
        Time.timeScale = 1f;  // Lanjutkan waktu (resume game)
        pausePanel.SetActive(false);  // Sembunyikan panel pause
        pauseButton.SetActive(true);  // Tampilkan tombol pause kembali
    }

    // Fungsi untuk kembali ke menu utama
    public void BackToMenu()
    {
        Time.timeScale = 1f;  // Pastikan waktu berjalan normal
        SceneManager.LoadScene("Game");  // Ganti dengan nama scene menu utama
    }

    // Coroutine untuk menunggu 3 detik sebelum tombol pause bisa diklik
    private IEnumerator EnablePauseButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Tunggu selama waktu yang ditentukan
        canPause = true;  // Tombol pause dapat diklik setelah delay selesai
        pauseButton.SetActive(true);  // Tampilkan tombol pause setelah 3 detik
    }
}
