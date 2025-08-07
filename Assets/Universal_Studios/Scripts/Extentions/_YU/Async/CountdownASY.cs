using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public static class CountdownASY
{
    static public async Task Countdown(int countdown)
    {
        for (int i = countdown; i > 0; i--)
        {
            Debug.Log("Countdown: " + i);
            await Task.Delay(1000); //Wait 1 Second
        }
        Debug.Log("Countdown Finished");
    }

    static public async Task CountdownWithBar(int countdown, Image image)
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
