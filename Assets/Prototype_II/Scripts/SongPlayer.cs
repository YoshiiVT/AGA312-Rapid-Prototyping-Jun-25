using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace PROTOTYPE_2
{
    public class SongPlayer : GameBehaviour  
    {
        [SerializeField] private SongData songData;
        [SerializeField] private List<BeatID> beatList;
        [SerializeField] private int startingBPM;
        [SerializeField] private SongID songID;

        [SerializeField, ReadOnly] private int BPM; //Beats per Minute
        [SerializeField, ReadOnly] private int BPS; //Beats per Second

        public void Initialize(SongData _songData)
        {
            songData = _songData;
            beatList = songData.beatList;
            startingBPM = songData.startingBPM;
            songID = songData.songID;

            BPM = startingBPM; //I made BPM different from startingBPM so that BPM could change freely while keeping track of what it started at
        }

        public void Update()
        {
            BPS = BPM / 60; //This convers BPM to seconds, and will continue to update if the beat quickens or slows
        }

        private IEnumerator BeatPlayer()
        {
            for (int i = 0; i < BPS; i++)
            {
                //This should should run as many beats there are in a second (So 2BPS, so this loop will run 2 times a second.
                //This is where the note moving script will go.

                //This could also have the logic that goes through the beat list, and can tell if the song ended.
            }
            yield return new WaitForSeconds(1);
            StartCoroutine(BeatPlayer());
        }

        //Add logic to play the stored beats in the beatList.
    }
}

