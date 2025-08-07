using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace PROTOTYPE_2
{
    public class SongPlayer : GameBehaviour<SongPlayer>  
    {
        [Header("Song Loaders")]
        [SerializeField] private SongData songData;
        [SerializeField] private List<BeatID> beatList;
        [SerializeField] private int startingBPM;
        [SerializeField] private SongID songID;

        [Header("BeatReferences")]
        [SerializeField] private List<BeatData> playableBeats;
        [SerializeField] private GameObject beatPrefab;

        [Header("Timing")]
        [SerializeField, ReadOnly] private float BPM; //Beats per Minute
        [SerializeField, ReadOnly] private float BPS; //Beats per Second
        [SerializeField, ReadOnly] private float SPB; //Seconds per Beat
        [SerializeField, ReadOnly] private int currentBeat = 0;

        [Header("DEV Controls")]
        [SerializeField] private bool isManual;
        [SerializeField, ReadOnly] private bool isLastNote;

        [Header("PlayArea")]
        [SerializeField] private Key startColumn;
        [SerializeField] private Canvas noteArea;
        [SerializeField] private GameObject noteContainer;
        [SerializeField] private Arrow arrow;

        [Header("GameVariables")]
        public List<GameObject> beatsInPlay;
        public bool playerNoteAlive;

        [Header("PlayerNoteReader")]
        [SerializeField] private GameObject playerNoteReader;
        [SerializeField] private GameManager _GM;

        //this is temp
        public void Start()
        {
            Initialize();
        }
        public void Initialize(/*SongData _songData*/)
        {
            //songData = _songData;
            beatList = songData.beatList;
            startingBPM = songData.startingBPM;
            songID = songData.songID;

            BPM = startingBPM; //I made BPM different from startingBPM so that BPM could change freely while keeping track of what it started at
            StartCoroutine(BeatPlayer());
        }

        public void Update()
        {
            if (isManual)
            {
                if (Input.GetKeyDown(KeyCode.Q)) { StartCoroutine(BeatPlayer()); /*Debug.Log("Moving Notes");*/ }
                if (Input.GetKeyDown(KeyCode.Alpha0)) { ManualSpawnNote(0); /*Debug.Log("Spawning Note");*/}
                if (Input.GetKeyDown(KeyCode.Alpha1)) { ManualSpawnNote(1); /*Debug.Log("Spawning Note");*/}
                if (Input.GetKeyDown(KeyCode.Alpha2)) { ManualSpawnNote(2); /*Debug.Log("Spawning Note");*/}
                if (Input.GetKeyDown(KeyCode.Alpha3)) { ManualSpawnNote(3); /*Debug.Log("Spawning Note");*/}
                if (Input.GetKeyDown(KeyCode.Alpha4)) { ManualSpawnNote(4); /*Debug.Log("Spawning Note");*/}
                if (Input.GetKeyDown(KeyCode.Alpha5)) { ManualSpawnNote(5); /*Debug.Log("Spawning Note");*/}
            }
            if (!isManual) { if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); } }  

            BPS = BPM / 60; //This convers BPM to seconds, and will continue to update if the beat quickens or slows
            SPB = 1 / BPS; //Converts BeatsPerSecond into SecondsPerBeat
        }

        private void SpawnNote(BeatData _beatData)
        {
            //Debug.LogWarning("Spawning in " +  _beatData);
            GameObject nextBeat = Instantiate(beatPrefab, startColumn.transform.position, Quaternion.identity);
            nextBeat.transform.SetParent(noteContainer.transform);
            nextBeat.GetComponent<BeatBehaviour>().Initialize(startColumn,_beatData, currentBeat);
            beatsInPlay.Add(nextBeat);
        }

        private IEnumerator BeatPlayer()
        {
            MoveNotes();

            yield return new WaitForSeconds(SPB);

            if (playerNoteReader.GetComponent<PlayerBeat>().IsNoteInCentre())
            { arrow.HitNote(SPB / 4); }

            if (playerNoteReader.GetComponent<PlayerBeat>().CentreNote())
            {
                Debug.LogWarning("Player Note in centre, waiting....");
                playerNoteReader.GetComponent<PlayerBeat>().ButtonToggle();
                yield return new WaitForSeconds(SPB);
                playerNoteReader.GetComponent<PlayerBeat>().ButtonToggle();
            }
            else yield return new WaitForSeconds(SPB);

                //This Logic goes through the beat list, spawning/initializing the next beat in line.
                BeatData beatToGet = BeatIDToData(GetNextBeat());

            SpawnNote(beatToGet);

            currentBeat++;

            if (!isManual)
            {
                if (!isLastNote)
                {
                    StartCoroutine(BeatPlayer()); //This loops the script over again if it isnt manual or last note 
                    yield return null;
                }
                else
                {
                    StartCoroutine(MoveEndNotes());
                }
            }
        }

        
        private BeatID GetNextBeat()
        {
            if(currentBeat + 1 == beatList.Count) { isLastNote = true; } //This will run EXACTLY on the last note, not after
            //Debug.LogWarning("Playing Beat " + beatList[currentBeat]);
            return beatList[currentBeat];
        }

        private BeatData BeatIDToData(BeatID _BeatID)
        {
            switch (_BeatID) //PlayerBeat = 0, PlayerSpeedUp = 1, PlayerSpeedDown = 2, StandardBeat = 3, SpeedUpBeat = 4, SpeedDownBeat = 5
            {
                case BeatID.PlayerBeat:
                    return playableBeats[0];
                case BeatID.PlayerSpeedUp:
                    return playableBeats[1];
                case BeatID.PlayerSpeedDown:
                    return playableBeats[2];
                case BeatID.StandardBeat:
                    return playableBeats[3];
                case BeatID.SpeedUpBeat:
                    return playableBeats[4];
                case BeatID.SpeedDownBeat:
                    return playableBeats[5];
                default : return null;
            }
        }

        private void MoveNotes()
        {
            //Debug.Log("Moving Notes");
            for (int i = 0; i < beatsInPlay.Count; i++)
            {
                //Debug.Log("Moving Note " + i);
                GameObject noteToMove = beatsInPlay[i];
                noteToMove.GetComponent<BeatBehaviour>().MoveNote(SPB);
            }
        }


        private void ManualSpawnNote(int i)
        {
            switch (i) 
            {
                case 0:
                    SpawnNote(playableBeats[0]);
                    break;
                case 1:
                    SpawnNote(playableBeats[1]);
                    break;
                case 2:
                    SpawnNote(playableBeats[2]);
                    break;
                case 3:
                    SpawnNote(playableBeats[3]);
                    break;
                case 4:
                    SpawnNote(playableBeats[4]);
                    break;
                case 5:
                    SpawnNote(playableBeats[5]);
                    break;

            }
        }

        private IEnumerator MoveEndNotes()
        {
            MoveNotes();

            yield return new WaitForSeconds(SPB);

            if (playerNoteReader.GetComponent<PlayerBeat>().IsNoteInCentre())
            { arrow.HitNote(SPB / 4); }

            yield return new WaitForSeconds(SPB);

            if (playerNoteReader.GetComponent<PlayerBeat>().CentreNote())
            {
                Debug.LogWarning("Player Note in centre, waiting....");
                playerNoteReader.GetComponent<PlayerBeat>().ButtonToggle();
                yield return new WaitForSeconds(SPB / 2);
                playerNoteReader.GetComponent<PlayerBeat>().ButtonToggle();
            }

            currentBeat++;
            if (beatsInPlay.Count >= 0) StartCoroutine(MoveEndNotes());
        }

        public void DestroyNote(int _beatToDestroy)
        {
            for (int i = beatsInPlay.Count - 1; i >= 0; i--)
            {
                GameObject beatObj = beatsInPlay[i];
                BeatBehaviour beat = beatObj.GetComponent<BeatBehaviour>();

                if (beat != null && beat.GetBeatOrder() == _beatToDestroy)
                {
                    beatsInPlay.RemoveAt(i);
                    Destroy(beatObj);
                    Debug.Log($"Destroyed beat with order {_beatToDestroy}");
                }
            }
        }

        public void IncreaseBPM()
        {
            BPM = BPM + 30;
        }

        public void DecreaseBPM()
        {
            BPM = BPM - 30;
        }

    }
}

