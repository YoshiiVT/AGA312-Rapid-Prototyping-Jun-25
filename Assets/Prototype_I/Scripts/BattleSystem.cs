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
    [SerializeField, ReadOnly] private GameState state;
    [SerializeField, ReadOnly] private int waveNumber = 1;
    [ReadOnly] public List<GameObject> unitList;


    private bool hasStartedNewRound = false;

    [Header("ManagerReferences")]
    [SerializeField, ReadOnly] private SpawnManager _SM;

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


    public int NewWave()
    {
        waveNumber++;
        return waveNumber;
    }

    public void NextWaveSpawned()
    {
        CheckWhoIsNext();
    }

    private void CheckWhoIsNext()
    {
        if (hasStartedNewRound) return;

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
                break;
            }
        }

        Debug.Log("No one found, Starting new round");
        hasStartedNewRound = true;
        // NewRound();
    }


    /*
    private void CheckWhoIsNext()
    {
        for(int i = 0; i < unitList.Count; i++)
        {
            GameObject unitToTest = unitList[i];
            if (unitToTest.GetComponent<Unit>().hadTurn == false) { CheckUnitType(unitToTest); Debug.Log("Checking: " + unitToTest + " " + unitList[i]); break; }
            else if (unitToTest.GetComponent<Unit>() == null) { Debug.LogError("Unit : " + unitList[i] + " did NOT have Unit Component"); break; }
        }
        Debug.Log("No one found, Starting new round");
        //NewRound();
    }
    */
    private void CheckUnitType(GameObject unitToTest)
    {
        Debug.Log("Checking UnitType...");
        if (unitToTest.GetComponentInChildren<PlayerController>())
        {
            state = GameState.PLAYER_TURN;
            Debug.Log("UnitType: PLAYER was found");
            HadTurn(unitToTest);
            CheckWhoIsNext();
        }
        else if (unitToTest.GetComponentInChildren<Enemy>())
        {
            state = GameState.ENEMY_TURN;
            Debug.Log("UnitType: ENEMY was found");
            HadTurn(unitToTest);
            CheckWhoIsNext();
        }
    }
    public void NewRound()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            GameObject unitToTest = unitList[i];
            unitToTest.GetComponent<Unit>().hadTurn = false;
        }
        CheckWhoIsNext();
    }

    public int FindCurrentWave()
    { return waveNumber; }

    private void HadTurn(GameObject unitToPass)
    {
        unitToPass.GetComponent<Unit>().hadTurn = true;
    }
}
