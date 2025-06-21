using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public enum GameState
{
    START, 
    BETWEEN_TURN,
    PLAYER_TURN, 
    ENEMY_TURN, 
    GAMEOVER
}
public enum TurnState
{
    WAITING,
    AIMING,
    PUSHING
}

public class BattleSystem : GameBehaviour
{
    #region (References and Variables)
    [SerializeField, ReadOnly] private GameState state;
    [SerializeField, ReadOnly] private TurnState tState;
    [SerializeField, ReadOnly] private int waveNumber = 1;
    [ReadOnly] public List<GameObject> unitList;

    [Header("ManagerReferences")]
    [SerializeField, ReadOnly] private SpawnManager _SM;
    [SerializeField, ReadOnly] private RigidBodyManager _RBM;
    #endregion

    #region (Unity Methods)
    private void Start()
    {
        //The Only Message BattleSystem gives to SpawnManager is "Hey! Game is starting, spawn the first wave" (Now also checks if enemies are left)
        GameObject gmObject1 = GameObject.Find("SpawnManager");
        _SM = gmObject1.GetComponent<SpawnManager>();
        //BattleSystem uses this to Manage all RigidBodies in the scene
        GameObject gmObject2 = GameObject.Find("RigidBodyManager");
        _RBM = gmObject2.GetComponent<RigidBodyManager>();

        state = GameState.START;
        tState = TurnState.WAITING;

        //This Finds the Player and puts them on the top of the list
        GameObject playerGO = GameObject.Find("Player");
        unitList.Add(playerGO);

        _SM.StartGame(waveNumber); //Tells SpawnManager to start game
        //StartCoroutine(FirstWaveDelay());
    }

    /// <summary>
    /// Temporary Input to help cycle through turns
    /// Stops it from all happening at once and stuck in a loop
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q)) { CheckWhoIsNext(); }
    }
    #endregion

   

    #region (Wave Managing Methods)
    /// <summary>
    /// This is used to incrament the wave number. Used by SpawnManager to set difficulty
    /// </summary>
    /// <returns></returns>
    public int NewWave()
    {
        waveNumber++;
        return waveNumber;
    }

    public int FindCurrentWave()
    { return waveNumber; }


    #endregion

    #region (Turn Based Gameplay Methods)

    /// <summary>
    /// Goes through all units in the list and sets their "hadTurn" to false, ready for next round
    /// </summary>
    public void NewRound()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            GameObject unitToTest = unitList[i];
            unitToTest.GetComponent<Unit>().hadTurn = false;
        }
        CheckWhoIsNext();
    }

    /// <summary>
    /// These two methods cycles through who's turn it currently is, has everyone had a turn, and acticates them if they havent and are next.
    /// </summary>
    private void CheckWhoIsNext()
    {
        if (CheckWhoHasntHadTurn() == true) { Debug.Log("Unit Found"); } //The Logic of having the selected unit do its actions will be in CheckUnitType()
        else { Debug.Log("No Unit Found, Starting new round."); NewRound(); } //Put Logic Here to Reset Units for new Round
    }

    private bool CheckWhoHasntHadTurn()
    {

        Debug.Log("Entered CheckWhoIsNext");
        for (int i = 0; i < unitList.Count; i++)
        {
            GameObject unitToTest = unitList[i];
            Debug.Log("Iterating: " + unitToTest.name);

            Unit unitComponent = unitToTest.GetComponent<Unit>();

            if (unitComponent == null)
            {
                Debug.LogError("Unit : " + unitToTest.name + " did NOT have Unit Component");
                break;
            }

            if (!unitComponent.hadTurn)
            {
                Debug.Log("Checking: " + unitToTest.name);
                CheckUnitType(unitToTest);
                return true;
            }
        }

        Debug.Log("No one found");
        return false;

    }

    /// <summary>
    /// This Identifies what type the selected unit is. (Is it a player or enemy?)
    /// Will also hold the logic for having that unit take its turn
    /// Right now it simply sets the selected unit "Had Turn" bool to true
    /// </summary>
    /// <param name="unitToTest"></param>
    private void CheckUnitType(GameObject unitToTest)
    {
        Debug.Log("Checking UnitType...");
        if (unitToTest.GetComponentInChildren<PlayerController>())
        {
            Debug.Log("UnitType: PLAYER was found");

            state = GameState.PLAYER_TURN;
            tState = TurnState.AIMING;
            
            unitToTest.GetComponentInChildren<PlayerController>().PlayerTurnStarts();
            HadTurn(unitToTest);
        }
        else if (unitToTest.GetComponentInChildren<Enemy>())
        {
            Debug.Log("UnitType: ENEMY was found");

            state = GameState.ENEMY_TURN;
            tState = TurnState.AIMING;

            unitToTest.GetComponentInChildren<Enemy>().EnemyTurnStarts();
            HadTurn(unitToTest);
        }
    }

    /// <summary>
    /// Sets selected units "hadTurn" to true, saying its had its turn
    /// </summary>
    /// <param name="unitToPass"></param>
    private void HadTurn(GameObject unitToPass)
    {
        unitToPass.GetComponent<Unit>().hadTurn = true;
    }

    
    #endregion

    public void UnitSubmitsPush(GameObject unitSubmission)
    {
        if (unitSubmission.GetComponentInParent<Unit>() == null) { Debug.LogError("Script not associated to either player or enemy tried to end turn"); return; } //Safety precaution: Only Players and Enemies should be able to call this script

        if (unitSubmission.GetComponent<Enemy>() != null && state == GameState.PLAYER_TURN) { Debug.LogError("Enemy tried to end Player's turn"); return; } //Safety precaution: Only Players can call this script during PLAYER_TURN

        if (unitSubmission.GetComponent<PlayerController>() != null && state == GameState.ENEMY_TURN) { Debug.LogError("Player tried to end Enemy's turn"); return; } //Safety precaution: Only Enemies can call this script during ENEMY_TURN
        
        tState = TurnState.PUSHING;
        StartCoroutine(PushDelay());
    }
    /// <summary>
    /// This method gives a 5 second grace period between pushing and then the WaitLoop()
    /// </summary>
    /// <returns></returns>
    private IEnumerator PushDelay()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(WaitLoop());
    }

    /// <summary>
    /// The Waitloop will check every object with Rigidbody in the scene to see if it is moving.
    /// If something is still moving, it waits a second before checking again.
    /// If nothing is moving, then it ends the loop and starts the next turn.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitLoop()
    {
        if (_RBM.SomethingMoving() == true)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(WaitLoop());
        }
        else
        {
            tState = TurnState.WAITING;
            if (_SM.AreEnemiesLeft() == true)
            {
                CheckWhoIsNext();
                yield return null;
            }
            else
            {
                NewWave();
                _SM.SpawnNextWave(gameObject);
            }

        }
        
    }

}
