using UnityEngine;
using System.Collections.Generic;

namespace PROTOTYPE_2
{
    public class SongPlayer : GameBehaviour  
    {
        [SerializeField] private SongData songData;
        [SerializeField] private List<BeatID> beatList;
        [SerializeField] private int startingBPM;
        [SerializeField] private SongID songID;

        [SerializeField, ReadOnly] private int BPM;

        public void Initialize(SongData _songData)
        {
            songData = _songData;
            beatList = songData.beatList;
            startingBPM = songData.startingBPM;
            songID = songData.songID;

            BPM = startingBPM / 2;
        }

        //Add logic to play the stored beats in the beatList.
    }
}

