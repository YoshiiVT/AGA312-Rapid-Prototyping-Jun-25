using UnityEngine;

namespace PROTOTYPE_5
{
    public class PlayerWeapon : MonoBehaviour
    {
        //[SerializeField] private WeaponData currentWeapon; [This will be for using scriptable objects for weapon types later

        [SerializeField] private Camera fpsCam;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float range = 100f;

        public void FireWeapon()
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.DrawLine(hit.point, hit.normal);
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if (target != null )
                {
                    target.TakeDamage(damage);
                }
            }
        }
    }

}
