using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "Songs", order = 1)]
public class SongData : ScriptableObject
{
    public List<GameObject> noteList = new List<GameObject>();
    public int startingBPM; //Average is 120BPM (2 beats a second)
}
