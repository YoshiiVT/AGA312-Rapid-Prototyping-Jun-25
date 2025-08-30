using UnityEngine;

public class FiringPoint : MonoBehaviour
{
    [SerializeField] private PlayerHealth target;

    [SerializeField] private ParticleSystem muzzleFlash;
    public void Fire()
    {
        Debug.Log("Gun Fired");

        muzzleFlash.Play();

        int rndHit = Random.Range(0, 4);

        if (rndHit == 3)
        {
            target.TakeDamage(5);
        }
    }

    
}
