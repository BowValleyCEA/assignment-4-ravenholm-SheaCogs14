using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject Pause;
    [SerializeField] private GameObject OverLevel;

    private bool _isPaused = false;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
    public void LevelTransition(string levelString)
    {
        SceneManager.LoadScene(levelString);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Paused()
    {
        Pause.SetActive(true);
        Time.timeScale = 0.0f;
        _isPaused = true;
    }

    public void Resume()
    {
        Pause.SetActive(false);
        Time.timeScale = 1.0f;
        _isPaused = false;
    }

    public void LevelOver()
    {
        OverLevel.SetActive(true);
        Time.timeScale = 0.0f;
    }
}