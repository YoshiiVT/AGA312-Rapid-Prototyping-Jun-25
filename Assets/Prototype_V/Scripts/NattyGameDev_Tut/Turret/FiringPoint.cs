using UnityEngine;

public class FiringPoint : MonoBehaviour
{
    [SerializeField] private PlayerHealth target;
    public void Fire()
    {
        Debug.Log("Gun Fired");

        int rndHit = Random.Range(0, 4);

        if (rndHit == 3)
        {
            target.TakeDamage(5);
        }
    }

    
}
