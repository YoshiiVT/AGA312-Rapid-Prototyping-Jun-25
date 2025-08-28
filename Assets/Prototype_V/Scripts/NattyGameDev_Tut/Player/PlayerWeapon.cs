using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PROTOTYPE_5
{
    public class PlayerWeapon : GameBehaviour
    {
        //[SerializeField] private WeaponData currentWeapon; [This will be for using scriptable objects for weapon types later

        [SerializeField] private Camera fpsCam;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float range = 100f;
        [SerializeField] private float fireRate = 10f;


        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private GameObject impactEffect;

        [SerializeField, ReadOnly] private bool firing;
        private Coroutine fireRoutine;

        [SerializeField] private List<AudioClip> fireSounds = new List<AudioClip>();
        [SerializeField, ReadOnly] private AudioSource fireSource;

        [SerializeField] private Animator animator; // handles animations

        private void Awake()
        {
            fireSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }

        public void FireWeapon()
        {
            firing = !firing;

            if (firing) { fireRoutine = StartCoroutine(FireLoop()); }
            else { StopCoroutine(fireRoutine); }
        }

        private IEnumerator FireLoop()
        {
            while (firing)
            {
                Shoot();
                yield return new WaitForSeconds(fireRate);
            }
        }

        private void Shoot()
        {
            AudioClip rndSound = ListX.GetRandomItemFromList(fireSounds);
            AudioSource.PlayClipAtPoint(rndSound, transform.position);

            // Play animation + effects
            animator.SetTrigger("Fire");
            muzzleFlash.Play();
            
            Debug.Log("Flash");

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.DrawLine(hit.point, hit.normal);
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                GameObject impactObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactObj, 2f);
            }
        }
    }

}
