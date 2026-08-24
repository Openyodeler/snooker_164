using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject This;

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
       This.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        This.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
