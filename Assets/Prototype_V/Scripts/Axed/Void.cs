using UnityEngine;

public class Void : MonoBehaviour
{
    public GameObject spawnPoint;
    public Timer timer;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.transform.position = spawnPoint.transform.position;
        timer.ResetTimer();
    }
}
