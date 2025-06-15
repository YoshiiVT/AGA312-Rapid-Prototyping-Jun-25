using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private GameObject player;

    [Header("Physics Variables")]
    public float speed = 5.0f;
    public float rotationSpeed;
    public float forwardInput;
    private Rigidbody enemyRb;
    [ReadOnly, SerializeField] private bool isMoving;
    [ReadOnly, SerializeField] private bool isPushed;
    [ReadOnly, SerializeField] private float currentSpeed;


    [Header("Referencess")]
    [SerializeField] private Component moveArrowImage;
    [SerializeField] private GameObject moveIndicator;

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

        moveIndicator.transform.position = transform.position;
        moveIndicator.transform.LookAt(player.transform.position); 
    }
}
