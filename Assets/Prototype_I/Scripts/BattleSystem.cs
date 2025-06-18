using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
    START, 
    PLAYER_TURN, 
    ENEMY_TURN, 
    WON, 
    LOST
}

public class BattleSystem : GameBehaviour
{
    #region (References and Variables)
    [SerializeField, ReadOnly] private GameState state;
    [SerializeField, ReadOnly] private int waveNumber = 1;
    [ReadOnly] public List<GameObject> unitList;

    [Header("ManagerReferences")]
    [SerializeField, ReadOnly] private SpawnManager _SM;
    #endregion

    #region (Unity Methods)
    private void Start()
    {
        //The Only Message BattleSystem gives to SpawnManager is "Hey! Game is starting, spawn the first wave"
        GameObject gmObject1 = GameObject.Find("SpawnManager");
        _SM = gmObject1.GetComponent<SpawnManager>();

        state = GameState.START;

        //This Finds the Player and puts them on the top of the list
        GameObject playerGO = GameObject.Find("Player");
        unitList.Add(playerGO);

        _SM.StartGame(waveNumber); //Tells SpawnManager to start game
    }

    /// <summary>
    /// Temporary Input to help cycle through turns
    /// Stops it from all happening at once and stuck in a loop
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q)) {CheckWhoIsNext(); }
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
            state = GameState.PLAYER_TURN;
            Debug.Log("UnitType: PLAYER was found");
            HadTurn(unitToTest);
        }
        else if (unitToTest.GetComponentInChildren<Enemy>())
        {
            state = GameState.ENEMY_TURN;
            Debug.Log("UnitType: ENEMY was found");
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
    #endregion

}
