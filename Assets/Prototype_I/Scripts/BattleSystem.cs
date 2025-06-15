using UnityEngine;
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

    private void Start()
    {
        state = GameState.START;
        GameObject playerGO = GameObject.Find("Player");
        unitList.Add(playerGO);
    }

    public int NewWave()
    {   
        waveNumber++;
        return waveNumber; 
    }
    public int FindCurrentWave()
    { return waveNumber; }
}
