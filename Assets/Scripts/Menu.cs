using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;

    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1f; 
    }

    public void Exit()
    {
        Application.Quit();
    }

}
