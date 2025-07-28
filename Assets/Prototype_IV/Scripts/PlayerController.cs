using UnityEngine;

namespace PROTOTYPE_4
{
    public class PlayerController : MonoBehaviour
    {
        public float velocity = 1;
        [SerializeField, ReadOnly] private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null) { Debug.LogError("Rigidbody NOT found"); }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Jump
                rb.linearVelocity = Vector2.up * velocity;
            }
        }
    }

}
