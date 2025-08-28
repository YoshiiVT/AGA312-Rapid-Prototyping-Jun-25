using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time;
    public TMP_Text timeText;
    private bool isTiming = true;

    void Update()
    {
        if (isTiming)
        {
            time += Time.deltaTime;
            UpdateTimeDisplay();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Stops time when a collider enters the trigger
        isTiming = false;
    }

    private void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void ResetTimer()
    {
        time = 0f;
        isTiming = true;
        UpdateTimeDisplay();
    }
}
