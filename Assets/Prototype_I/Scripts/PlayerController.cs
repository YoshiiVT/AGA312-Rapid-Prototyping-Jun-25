using System.Collections;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    # region(References)

    [Header("Input Manager")]
    public InputAction arrowControls;
    public InputAction moveControls;

    [Header("Physics Variables")]
    public float speed = 5.0f;
    public float rotationSpeed;
    public float forwardInput;
    private Rigidbody playerRb;
    [ReadOnly, SerializeField] private bool isMoving;
    [ReadOnly, SerializeField] private bool isPushed;
    [ReadOnly, SerializeField] private float currentSpeed;

    [Header("Referencess")]
    [SerializeField] private GameObject moveArrow; private Component moveArrowImage;
    [ReadOnly, SerializeField] private GameObject moveIndicator;

    [Header("PowerUP Variables")]
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    private float powerupDuration = 7f;
    public GameObject powerupIndicator;

    [Header("TurnBased Variables")]
    [ReadOnly, SerializeField] private bool playerTurn = false;
    [ReadOnly, SerializeField] private GameObject battleSystem;
    #endregion

    #region(Onload / Start)
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
        moveArrow = GameObject.Find("Arrow");
        moveArrowImage = moveArrow.GetComponent<Image>();
        moveArrow.SetActive(false);
        battleSystem = GameObject.Find("GameManager");
    }
    #endregion(Onload / Start)

    public void PlayerTurnStarts()
    {
        moveArrow.SetActive(true);
        playerTurn = true;
    }

    #region (Movement Methods)
    void Update()
    {      
        moveIndicator.transform.position = transform.position;

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        float horizontalInput = arrowControls.ReadValue<float>();
        moveIndicator.transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        currentSpeed = RigidBodyX.GetSpeedRB(playerRb);

        if (!isMoving) { isPushed = false; }
        if (isMoving)
        {
   
            moveArrow.GetComponent<Image>().color = Color.grey;

            if (currentSpeed >= 1.1) { isPushed = false; Debug.Log("Player no longer pushed"); }

            if (!isPushed && isMoving && currentSpeed <= 1)
            {
                if (currentSpeed <= 0) { isMoving = false; Debug.Log("Player no longer moving"); }

                RigidBodyX.ForceStopRB(playerRb);
                Debug.Log("Forcestopping player");
            }

        }
        else ColorX.SetColorFromHex(moveArrowImage, "#C8FFC6"); //moveArrow.GetComponent<Image>().color = Color.green;


        if (!playerTurn) //If it is not the players turn, Do not regester any input.
        {
            return;
        }

        //float forwardInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isMoving)
            {
                playerRb.AddForce(moveIndicator.transform.forward * speed * forwardInput);
                isMoving = true; isPushed = true;
                battleSystem.GetComponent<BattleSystem>().UnitSubmitsPush(gameObject);
                PlayerTurnEnds();
            }
            else print("Can't push while moving");
        }
    }
    #endregion


    #region (Powerup Methods)
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

        if (collision.gameObject.CompareTag("Enemy") && !playerTurn)
        {
            isMoving = true; isPushed = true; Debug.Log("Player Pushed By an Enemy");
        }
    }
    #endregion


    private void PlayerTurnEnds()
    {
        moveArrow.SetActive(false);
        playerTurn = false;
    }
}
