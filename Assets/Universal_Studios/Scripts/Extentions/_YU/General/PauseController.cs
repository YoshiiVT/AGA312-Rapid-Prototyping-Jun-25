using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    private bool paused;

    private void Start()
    {
        paused = false;
        pausePanel.SetActive(paused);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    public void Pause()
    {
        paused = !paused;
        pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0 : 1; //If paused is true 0, else 1;
    }
}
