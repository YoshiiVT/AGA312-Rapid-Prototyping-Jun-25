using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input Manager")]
    public InputAction arrowControls;
    public InputAction moveControls;

    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject moveIndicator;
    public float rotationSpeed;
    public float forwardInput;

    [Header("PowerUP Variables")]
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    private float powerupDuration = 7f;
    public GameObject powerupIndicator;

    private void OnEnable()
    {
        arrowControls.Enable();
    }

    private void OnDisable()
    {
        arrowControls.Disable();
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        moveIndicator = GameObject.Find("MoveIndicator");
    }
    
    void Update()
    {
        //float forwardInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(moveIndicator.transform.forward * speed * forwardInput);
        }
        

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        float horizontalInput = arrowControls.ReadValue<float>();
        moveIndicator.transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse); //ForceMode.Impulse?        
        }
    }
}
