using PROTOTYPE_3;
using UnityEngine;

public class Bowl : GameBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChuteOBJ>() != null)
        {
            _CM.addBowl(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
