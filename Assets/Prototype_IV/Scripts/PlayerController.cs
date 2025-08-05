using Unity.Mathematics;
using UnityEngine;

namespace PROTOTYPE_4
{
    public class PlayerController : MonoBehaviour
    {
        //Temp
        private GameManager gameManager;

        public float velocity = 1;
        [SerializeField] private Rigidbody2D rb;

        private GameObject lastCollidedObj;

        void Start()
        {
            //Temp
            GameObject gameManagerobj = GameObject.Find("GameManager");
            gameManager = gameManagerobj.GetComponent<GameManager>();

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Jump
                rb.linearVelocity = Vector2.up * velocity;
            }

            transform.rotation = Quaternion.Euler(0, 0, rb.linearVelocity.y * 4 - 90);

        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Collision"))
            {
                Debug.Log("Collision Detected");
                lastCollidedObj = collision.gameObject;
                gameManager.Death();
            }
            else { Debug.LogWarning("Something Detected"); }
        }

        public void DestroyLastCollidedObj()
        {
            GameObject lastCollidedObjParent = lastCollidedObj.transform.parent.gameObject;
            Destroy(lastCollidedObjParent);
        }
    }

}
