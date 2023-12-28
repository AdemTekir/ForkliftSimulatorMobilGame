using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1.0f;
    }

    public void LoadLevel1Scene()
    {
        SceneManager.LoadScene("Level1Scene");
        Time.timeScale = 1.0f;
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
    }

    public void StopGame ()
    {
        Time.timeScale = 0f;
    }
}
