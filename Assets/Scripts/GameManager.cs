using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int frogsFinished = 0; // Counter for frogs that finished the game
    public int totalFrogs = 2; // Total number of frogs

    public int maxLives = 3; // Jumlah nyawa awal
    private int currentLives; // Nyawa tersisa

    void Start()
    {
        currentLives = maxLives; // Inisialisasi nyawa
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
    }

    private void GameWon()
    {
        Debug.Log("Semua katak telah mencapai garis finish! Game selesai.");
        Time.timeScale = 0; // Pause the game
    }

    public int GetCurrentLives()
    {
        return currentLives; // Bisa dipanggil untuk menampilkan nyawa di UI
    }
}
