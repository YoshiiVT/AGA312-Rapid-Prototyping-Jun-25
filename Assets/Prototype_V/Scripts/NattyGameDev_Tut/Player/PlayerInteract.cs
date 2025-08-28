using UnityEngine;

namespace PROTOTYPE_5
{
    public class PlayerInteract : MonoBehaviour
    {
        [Header("Script References")]
        [SerializeField, ReadOnly] private Camera cam;
        [SerializeField, ReadOnly] private PlayerUI playerUI;
        [SerializeField, ReadOnly] private InputManager inputManager;

        [Header("Variables")]
        [SerializeField] private float distance = 3f;
        [SerializeField] private LayerMask mask;
        

        private void Start()
        {
            cam = GetComponent<PlayerLook>().Cam;
            if (cam == null) { Debug.LogError("Camera returned NULL"); }

            playerUI = GetComponent<PlayerUI>();
            if (playerUI == null) { Debug.LogError("Player UI returned NULL"); }

            inputManager = GetComponent<InputManager>();
            if (inputManager == null) { Debug.LogError("Input Manager returned NULL"); }
        }

        private void Update()
        {
            playerUI.UpdateText(string.Empty);
            //create a ray at the center of the camera, shooting outwards
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * distance);
            RaycastHit hitInfo; // variable to store our collision infomation.
            if (Physics.Raycast(ray, out hitInfo, distance, mask))
            {
                if(hitInfo.collider.GetComponent<Interactable>() != null)
                {
                    Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

                    playerUI.UpdateText(interactable.PromptMessage);
                    //Debug.Log(hitInfo.collider.GetComponent<Interactable>().PromptMessage);

                    if (inputManager.OnFoot.Interact.triggered)
                    {
                        interactable.BaseInteract();
                    }
                }
            }
        }
    }
} 

