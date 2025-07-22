using PROTOTYPE_3;
using UnityEngine;

public class Bowl : GameBehaviour
{
    [SerializeField] private GameObject batter;
    private bool batterToggle;
    public void Start()
    {
        batter.SetActive(false);
        batterToggle = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChuteOBJ>() != null)
        {
            _CM.addBowl(other.gameObject);
            other.gameObject.SetActive(false);

            if(batterToggle == false)
            {
                batter.SetActive(true);
                batterToggle = true;
            }

        }
    }
}
