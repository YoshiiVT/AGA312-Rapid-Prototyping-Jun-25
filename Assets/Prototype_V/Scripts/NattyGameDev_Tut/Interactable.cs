using UnityEngine;

namespace PROTOTYPE_5
{
    //So basically allows us to make subclasses
    //https://dotnettutorials.net/lesson/template-method-design-pattern/ use this to learn more
    public abstract class Interactable : MonoBehaviour
    {
        //Message displayed to player when looking at an interactable
        [SerializeField] private string promptMessage;
        public string PromptMessage => promptMessage;

        //this function will be called from our player.
        public void BaseInteract()
        {
            Interact();
        }

        protected virtual void Interact()
        {
            //We wont have any code written in this function
            //this is a template function to be overridden by our sublcass
        }

    }

}
