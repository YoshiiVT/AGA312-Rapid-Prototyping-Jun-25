using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState
{
    Waiting,
    Aiming,
    Moving
}

public class Enemy : MonoBehaviour
{
    #region(References)
    [Header("Physics Variables")]
    public float speed = 3.0f;
    public float rotationSpeed;
    public float forwardInput;
    private Rigidbody enemyRb;
    [ReadOnly, SerializeField] private bool isMoving;
    [ReadOnly, SerializeField] private bool isPushed;
    [ReadOnly, SerializeField] private float currentSpeed;
    [ReadOnly, SerializeField] private EnemyState enemyState;

    [Header("Arrow Referencess")]
    [SerializeField] private GameObject moveIndicator;
    [SerializeField] private GameObject moveArrow;
    [SerializeField] private Component moveArrowImage;

    [Header("Aiming Refereces")]
    [ReadOnly, SerializeField] private GameObject player;
    [ReadOnly, SerializeField] private bool isAiming = false;
    [ReadOnly, SerializeField] private Vector3 aimPoint;
    [SerializeField] private int accuracy = 5; //The lower the number the higher the accuracy


    [Header("Turn Variables")]
    [ReadOnly, SerializeField] private bool enemyTurn = false;

    [Header("ManagerReferences")]
    [SerializeField, ReadOnly] private BattleSystem _BS;
    [SerializeField, ReadOnly] private SpawnManager _SM;
    #endregion

    #region(OnLoad / Start)
    void Start()
    {
        GameObject gmObject = GameObject.Find("GameManager");
        _BS = gmObject.GetComponent<BattleSystem>();

        GameObject gmObject1 = GameObject.Find("SpawnManager");
        _SM = gmObject1.GetComponent<SpawnManager>();

        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("PlayerMesh");

        moveArrow.SetActive(false);
    }

    public void EnemyTurnStarts()
    {
        moveArrow.SetActive(true);
        enemyTurn = true;
    }

    /* //Old logic from when I was making the pushed based movement system
    private IEnumerator TempAi()
    {
        yield return new WaitForSeconds(10);
        EnemyAiPush();
        StartCoroutine(TempAi());
    }
    */
    #endregion

    #region(Movement Methods)
    void Update()
    {
        //This will despawn the ball if it falls below -10, and lets the SpawnManager know
        if (transform.position.y <= -10) 
        {
            Debug.Log("EnemyFell");
            GameObject enemyParentGO = transform.parent?.gameObject; //Find the parent object this object is under
            _BS.unitList.Remove(enemyParentGO);
            //_BS.GetComponent<BattleSystem>().UnitSubmitsPush(gameObject);
            Destroy(enemyParentGO);
        }

        currentSpeed = RigidBodyX.GetSpeedRB(enemyRb); //Gets the current velocity (Speed) of the rigidbody
        moveIndicator.transform.position = transform.position; //Keeps the centerpoint of the moveArrow to the centre of the ball

        if (!isMoving) { isPushed = false; }
        if (isMoving)
        {

            moveArrow.GetComponent<Image>().color = Color.grey;

            if (currentSpeed >= 1.1)
            {
                isPushed = false; Debug.Log("Enemy no longer pushed"); }

                if (!isPushed && isMoving && currentSpeed <= 1)
                {
                    if (currentSpeed <= 0)
                    {
                        isMoving = false; Debug.Log("Enemy no longer moving"); }

                        RigidBodyX.ForceStopRB(enemyRb);
                        Debug.Log("Forcestopping Enemy");
                    }

                }
                else ColorX.SetColorFromHex(moveArrowImage, "#C8FFC6"); //moveArrow.GetComponent<Image>().color = Color.green;


                switch (enemyState) //This Switch statement will be the core of the enemy Ai, cycling through 3 states
        {
            case EnemyState.Waiting:
                {
                    if (!enemyTurn) {return;}
                    else
                    {
                        enemyState = EnemyState.Aiming;
                        break;
                    }
                }
            case EnemyState.Aiming:
                {
                    moveIndicator.transform.LookAt(aimPoint);

                    if (!isAiming)
                    {
                        Debug.Log("Enemy is Aiming");
                        StartCoroutine(EnemyAiming());
                        isAiming = true;
                    }

                    /* //This is stupid as I can just change aimPoint using a Random.Range()
                    Vector3 aimRangeA = aimPoint + new Vector3(5f, 0f, 0f);
                    Vector3 aimRangeB = aimPoint + new Vector3(-5f, 0f, 0f);
                    */
                    break;
                }
            case EnemyState.Moving:
                {
                    break;
                }
        }

        /* //This Logic and needs to be Updated like the Player's, and suited for the Ai
        if (isMoving)
        {
            moveArrowImage.GetComponent<Image>().color = Color.grey;
            if (currentSpeed >= 1) isPushed = false;

            if (!isPushed && isMoving && currentSpeed <= 1)
            {
                StartCoroutine(WaitForForceStop(enemyRb));
            }
        }
        else ColorX.SetColorFromHex(moveArrowImage, "#C8FFC6"); //moveArrow.GetComponent<Image>().color = Color.green;
        */
    }

    /* //This Logic and needs to be Updated like the Player's, and suited for the Ai
    private void EnemyAiPush() 
    {
        if (!isMoving)
        {
            enemyRb.AddForce(moveIndicator.transform.forward * speed * forwardInput);
            isMoving = true; isPushed = true;
        }
        else print("Enemy AI tried to push while moving");
    }
    */

    /* //This Logic and needs to be Updated like the Player's, and suited for the Ai
    private IEnumerator WaitForForceStop(Rigidbody enemyrRb)
    {
        yield return StartCoroutine(RigidBodyX.ForceStopGraduallyRB(enemyRb));
        isMoving = false;
    }
    */
    #endregion

    private IEnumerator EnemyAiming() //Chat helped me with this bit!
    {
        Vector3 baseTarget = player.transform.position;
        float time = 0f;
        float duration = 3f;
        float bounceSpeed = 5f; // how fast it bounces
        float bounceWidth = accuracy * 2; // how far left/right it bounces

        while (time < duration)
        {
            // Horizontal side-to-side sine bounce around the base target
            float xOffset = Mathf.Sin(time * bounceSpeed) * bounceWidth;
            aimPoint = baseTarget + new Vector3(xOffset, 0f, 0f);

            time += Time.deltaTime;
            yield return null;
        }

        // After 3 seconds, apply final deviation
        float xDeviation = Random.Range(-accuracy, accuracy);
        float zDeviation = Random.Range(-accuracy, accuracy);
        aimPoint = baseTarget + new Vector3(xDeviation, 0f, zDeviation);

        enemyState = EnemyState.Moving; // Change to Moving Later, or get rid of moving?
        isAiming = false; // So you can re-enter aiming later if needed
        EnemyPushing();
    }

    private void EnemyPushing()
    {
        Debug.Log("Enemy is Pushing");
        enemyRb.AddForce(moveIndicator.transform.forward * speed * forwardInput);
        isMoving = true; isPushed = true;
        _BS.GetComponent<BattleSystem>().UnitSubmitsPush(gameObject);
        EnemyTurnEnds();
    }

    /*
    private IEnumerator EnemyTurnLifetime()
    {
        yield return new WaitForSeconds(1);
        EnemyTurnEnds();
    }
    */

    private void EnemyTurnEnds()
    {
        moveArrow.SetActive(false);
        enemyTurn = false;
        enemyState = EnemyState.Waiting;
    }
}
