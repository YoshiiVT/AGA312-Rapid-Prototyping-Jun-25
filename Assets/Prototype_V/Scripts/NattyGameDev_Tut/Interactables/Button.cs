using UnityEngine;

namespace PROTOTYPE_5
{
    public class Button : Interactable
    {
        [SerializeField] GameObject door;
        [SerializeField, ReadOnly] private bool doorOpen;

        private void Awake()
        {
            if (door == null) { Debug.LogError("Door returned NULL"); }
        }

        //this function is where we will design our interaction using code.
        protected override void Interact()
        {
            doorOpen = !doorOpen;
            door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
            Debug.Log("Interacted with " + gameObject.name);
        }
    }
}

