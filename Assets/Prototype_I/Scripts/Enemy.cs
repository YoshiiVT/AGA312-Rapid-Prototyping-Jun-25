using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    [SerializeField] private GameObject player;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("PlayerMesh");
    }

    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce((lookDirection * speed));  //Look up what normalized means later

        if (transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }
}
