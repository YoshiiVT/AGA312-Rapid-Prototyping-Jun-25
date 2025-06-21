using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class P1MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Prototype 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
