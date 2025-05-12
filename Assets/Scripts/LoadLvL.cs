using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvL : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOverScreen");
    }
}
