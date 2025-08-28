using UnityEngine;

namespace PROTOTYPE_5
{
    public class Target : MonoBehaviour
    {
        public float health = 50f;

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
            Destroy(gameObject);
        }

    }
}

