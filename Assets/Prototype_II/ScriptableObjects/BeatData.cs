using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Beat", menuName = "Beats", order = 1)]
public class BeatData : ScriptableObject
{
    public bool playerBeat; //If True, then it is a beat that the player has to hit
    public bool speedUpBPM; //If True, then the BPM is increased for the next beat
    public bool speedDownBPM; //If True, then the BPM is decreased for the next beat
    
    public BeatID beatID;

}
public enum BeatID
{
    PlayerBeat, PlayerSpeedUp, PlayerSpeedDown, StandardBeat, SpeedUpBeat, SpeedDownBeat
}
