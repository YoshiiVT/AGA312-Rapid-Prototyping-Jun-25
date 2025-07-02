using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "Songs", order = 1)]
public class SongData : ScriptableObject
{
    public List<BeatID> beatList = new List<BeatID>();
    public int startingBPM; //Average is 120BPM (2 beats a second)
    public SongID songID;
}

public enum SongID
{
    TutorialSong, Song1, Song2, Song3, TestSong
}
