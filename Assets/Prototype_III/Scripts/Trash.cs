using PROTOTYPE_3;
using UnityEngine;

public class Trash : GameBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChuteOBJ>() != null)
        {
            _CM.addTrash(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
