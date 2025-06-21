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
    public float speed = 5.0f;
    public float rotationSpeed;
    public float forwardInput;
    private Rigidbody enemyRb;
    [ReadOnly, SerializeField] private bool isMoving;
    [ReadOnly, SerializeField] private bool isPushed;
    [ReadOnly, SerializeField] private float currentSpeed;
    [ReadOnly, SerializeField] private EnemyState enemyState;

    [Header("Referencess")]
    [ReadOnly, SerializeField] private GameObject player;
    [SerializeField] private GameObject moveIndicator;
    [SerializeField] private GameObject moveArrow;
    [SerializeField] private Component moveArrowImage;
    

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

        StartCoroutine(EnemyTurnLifetime()); //This is Temporary, it gives the enemy ai three seconds for a turn before ending turn.
        _BS.GetComponent<BattleSystem>().UnitSubmitsPush(gameObject);
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
            Destroy(enemyParentGO);
        }

        currentSpeed = RigidBodyX.GetSpeedRB(enemyRb); //Gets the current velocity (Speed) of the rigidbody
        moveIndicator.transform.position = transform.position; //Keeps the centerpoint of the moveArrow to the centre of the ball

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
                    moveIndicator.transform.LookAt(player.transform.position);
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

    private IEnumerator EnemyTurnLifetime()
    {
        yield return new WaitForSeconds(3);
        EnemyTurnEnds();
    }

    private void EnemyTurnEnds()
    {
        moveArrow.SetActive(false);
        enemyTurn = false;
    }
}
