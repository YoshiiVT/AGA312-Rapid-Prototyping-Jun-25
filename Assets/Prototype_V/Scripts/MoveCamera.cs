using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //The Camera holder method is uses as it can provide a smoother feel when using rigidbodies

    [SerializeField] private Transform cameraPosition;
    
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
