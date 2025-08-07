using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Image imageExample;

    private async void Start()
    {
        Time.timeScale = 0;
        await CountdownWithBar(5, imageExample);
    }

    private async Task CountdownWithBar(int countdown, Image image)
    {
        float elapsed = 0f;
        int updateRateMS = 10; // how often to update (10ms = 100 FPS)

        while (elapsed < countdown)
        {
            await Task.Delay(updateRateMS);

            elapsed += updateRateMS / 1000f;
            float fill = Mathf.Clamp01(1f - (elapsed / countdown));
            image.fillAmount = fill;
        }

        image.fillAmount = 0f;
        Debug.Log("Countdown Finished");
    }
}
