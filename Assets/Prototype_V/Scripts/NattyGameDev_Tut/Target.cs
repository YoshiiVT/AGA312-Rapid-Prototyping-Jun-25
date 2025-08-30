using System.Collections;
using UnityEngine;

namespace PROTOTYPE_5
{
    public class Target : MonoBehaviour
    {
        public float health = 50f;

        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private bool endGame;
        [SerializeField] private PlayerHealth ph;

        public void TakeDamage (float _damage)
        {
            health -= _damage;
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die ()
        {
            GameObject Explosion = Instantiate(explosionPrefab);
            Explosion.transform.position = gameObject.transform.position;
            Explosion.GetComponent<ParticleSystem>().Play();

            if (endGame ) { ph.End(); }

            Destroy(gameObject);

        }
    }
}

