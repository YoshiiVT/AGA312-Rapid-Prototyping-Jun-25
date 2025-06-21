using System.Collections;
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
    [SerializeField] private int powerupDuration = 1;
    private int durationLeft;
    public GameObject powerupIndicator;

    [Header("TurnBased Variables")]
    [ReadOnly, SerializeField] private bool playerTurn = false;
    [ReadOnly, SerializeField] private GameObject battleSystem;
    private bool pushedThisTurn = false;
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
        PowerUpDurationDecreaser();
    }

    #region (Movement Methods)
    void Update()
    {
        //This will despawn the ball if it falls below -10, and lets the SpawnManager know
        if (transform.position.y <= -10)
        {
            Debug.Log("Player Fell");
            GameObject enemyParentGO = transform.parent?.gameObject;
            battleSystem.GetComponent<BattleSystem>().GameOver(gameObject);
            Destroy(enemyParentGO);
        }

        moveIndicator.transform.position = transform.position;

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        float horizontalInput = arrowControls.ReadValue<float>();
        moveIndicator.transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        currentSpeed = RigidBodyX.GetSpeedRB(playerRb);

        if (!isMoving) { isPushed = false; }
        if (isMoving)
        {
   
            moveArrow.GetComponent<Image>().color = Color.grey;

            if (currentSpeed >= 1.1) { isPushed = false; } //Debug.Log("Player no longer pushed"); 

            if (!isPushed && isMoving && currentSpeed <= 1)
            {
                if (currentSpeed <= 0) { isMoving = false;} //Debug.Log("Player no longer moving"); 

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
            if (!isMoving && !pushedThisTurn)
            {
                playerRb.AddForce(moveIndicator.transform.forward * speed * forwardInput);
                isMoving = true; isPushed = true;
                StartCoroutine(PlayerTurnLifetime());
                pushedThisTurn = true;
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
            durationLeft = powerupDuration;
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void PowerUpDurationDecreaser()
    {
        if (durationLeft == 0)
        {
            hasPowerup = false;
            powerupIndicator.gameObject.SetActive(false);
        }
        else { durationLeft--; }
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

    private IEnumerator PlayerTurnLifetime()
    {
        yield return new WaitForSeconds(3);
        battleSystem.GetComponent<BattleSystem>().UnitSubmitsPush(gameObject);
        PlayerTurnEnds();
    }
    private void PlayerTurnEnds()
    {
        moveArrow.SetActive(false);
        pushedThisTurn = false;
        playerTurn = false;
    }
}
