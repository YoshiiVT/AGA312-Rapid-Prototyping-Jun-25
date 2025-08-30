using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    private void OnTriggerExit(Collider other)
    {
        doorAnimator.SetBool("IsOpen", false);
    }
}
