using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

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

        [SerializeField] private int maxAmmo = 24;
        [SerializeField, ReadOnly] private int currentAmmo;

        [SerializeField, ReadOnly] private bool firing;
        private Coroutine fireRoutine;

        [SerializeField, ReadOnly] private bool isReloading;

        [SerializeField] private List<AudioClip> fireSounds = new List<AudioClip>();
        [SerializeField, ReadOnly] private AudioSource fireSource;

        [SerializeField] private AudioClip magIn;
        [SerializeField] private AudioClip magOut;

        [SerializeField] private TMP_Text AmmoCount;

        [SerializeField] private Animator animator; // handles animations

        private void Awake()
        {
            fireSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            currentAmmo = maxAmmo;
        }

        private void Update()
        {
            AmmoCount.text = currentAmmo.ToString();

            if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                StartCoroutine(Reloading());
            }
        }

        private IEnumerator Reloading()
        {
            isReloading = true;
            animator.SetBool("Reloading", true);
            fireSource.PlayOneShot(magOut);
            yield return new WaitForSeconds(0.7f);
            animator.SetBool("Reloading", false);
            fireSource.PlayOneShot(magIn);
            yield return new WaitForSeconds(0.3f);
            currentAmmo = maxAmmo;
            isReloading = false;
        }

        public void FireWeapon()
        {
            firing = !firing;

            if (!isReloading)
            {
                if (firing) { fireRoutine = StartCoroutine(FireLoop()); }
                else { StopCoroutine(fireRoutine); }
            }
        }

        private IEnumerator FireLoop()
        {
            while (firing)
            {
                if (isReloading)
                {
                    break;
                }

                Shoot();
                yield return new WaitForSeconds(fireRate);
            }
        }

        private void Shoot()
        {
            --currentAmmo;

            AudioClip rndSound = ListX.GetRandomItemFromList(fireSounds);
            fireSource.PlayOneShot(rndSound);

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

            if (currentAmmo <= 0 && !isReloading)
            {
                StartCoroutine(Reloading());
            }
        }
    }

}
