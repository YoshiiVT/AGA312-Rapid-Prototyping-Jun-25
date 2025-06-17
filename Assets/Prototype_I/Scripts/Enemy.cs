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
    
    [SerializeField] private GameObject player;

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
    [SerializeField] private Component moveArrowImage;
    [SerializeField] private GameObject moveIndicator;

    [Header("Turn Variables")]
    [ReadOnly, SerializeField] private bool hadTurn = false;

    [Header("ManagerReferences")]
    [SerializeField, ReadOnly] private BattleSystem _BS;
    [SerializeField, ReadOnly] private SpawnManager _SM;
    void Start()
    {
        GameObject gmObject = GameObject.Find("GameManager");
        _BS = gmObject.GetComponent<BattleSystem>();

        GameObject gmObject1 = GameObject.Find("SpawnManager");
        _SM = gmObject1.GetComponent<SpawnManager>();

        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("PlayerMesh");
    }

    private IEnumerator TempAi()
    {
        yield return new WaitForSeconds(10);
        EnemyAiPush();
        StartCoroutine(TempAi());
    }

    void Update()
    {
        if (transform.position.y <= -10)
        {
            GameObject enemyParentGO = transform.parent?.gameObject; //Find the parent object this object is under
            _BS.unitList.Remove(enemyParentGO);
            _SM.CheckEnemyCount();
            Destroy(enemyParentGO);
        }

        currentSpeed = RigidBodyX.GetSpeedRB(enemyRb);
        moveIndicator.transform.position = transform.position;

        switch (enemyState)
        {
            case EnemyState.Waiting:
                {
                    if (!hadTurn) {return;}
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
    }

    private void EnemyAiPush()
    {
        if (!isMoving)
        {
            enemyRb.AddForce(moveIndicator.transform.forward * speed * forwardInput);
            isMoving = true; isPushed = true;
        }
        else print("Enemy AI tried to push while moving");
    }

    private IEnumerator WaitForForceStop(Rigidbody enemyrRb)
    {
        yield return StartCoroutine(RigidBodyX.ForceStopGraduallyRB(enemyRb));
        isMoving = false;
    }
}
