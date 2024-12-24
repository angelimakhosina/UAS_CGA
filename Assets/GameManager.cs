using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int frogsFinished = 0; // Counter for frogs that finished the game
    public int totalFrogs = 2; // Total number of frogs

    public void FrogFinished()
    {
        frogsFinished++;

        // Check if all frogs have finished
        if (frogsFinished >= totalFrogs)
        {
            GameWon();
        }
    }

    private void GameWon()
    {
        Debug.Log("Semua katak telah mencapai garis finish! Game selesai.");
        Time.timeScale = 0; // Pause the game
    }
}