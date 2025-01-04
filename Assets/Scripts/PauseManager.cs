using UnityEngine;
using UnityEngine.SceneManagement; // Tambahkan ini

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;  // Panel yang berisi tombol Resume dan Back to Menu
    public GameObject pauseButton; // Tombol Pause yang ada di UI utama

    // void Start()
    // {
    //     // Pastikan panel pause disembunyikan saat game dimulai
    //     pausePanel.SetActive(false);
    // }

    // Fungsi untuk mengaktifkan menu pause
    public void PauseGame()
    {
        Time.timeScale = 0f;  // Hentikan waktu (pause game)
        pausePanel.SetActive(true);  // Tampilkan panel pause
        pauseButton.SetActive(false); // Sembunyikan tombol pause
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
}
