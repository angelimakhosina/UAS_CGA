using UnityEngine;
using UnityEngine.SceneManagement;  // Import untuk Scene Management

public class MenuController : MonoBehaviour
{
    public void OnEasyButtonClicked()
    {
        // Ganti dengan nama scene level easy Anda
        SceneManager.LoadScene("LevelEasy");
    }

    public void OnHardButtonClicked()
    {
        SceneManager.LoadScene("LevelHard");
    }
}
