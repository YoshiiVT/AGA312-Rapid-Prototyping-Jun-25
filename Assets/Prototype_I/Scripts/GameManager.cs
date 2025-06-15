using UnityEngine;

public class GameManager : GameBehaviour
{
    [SerializeField, ReadOnly] private int waveNumber = 1;

    public int NewWave()
    {   
        waveNumber++;
        return waveNumber; 
    }
    public int FindCurrentWave()
    { return waveNumber; }
}
