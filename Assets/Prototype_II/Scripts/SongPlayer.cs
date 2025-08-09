using System.Collections.Generic;
using UnityEngine;

namespace PROTOTYPE_2
{
    public class SongPlayer : MonoBehaviour
    {
        [Header("Song Loaders")]
        [SerializeField] private SongData songData; //This is where the scriptableObject holding the song is inserted
        [SerializeField, ReadOnly] private List<GameObject> notesToPlay; //From which I gives the notes that are in the song and their order
        [SerializeField, ReadOnly] private int startingBPM; //And the starting BPM;

        [Header("Timing")]
        [SerializeField, ReadOnly] private float BPM; //Beats per Minute
        [SerializeField, ReadOnly] private float BPS; //Beats per Second
        [SerializeField, ReadOnly] private float SPB; //Seconds per Beat
        [SerializeField, ReadOnly] private int currentNote = 0; //Keeps track of how many notes came before it

        [Header("PlayArea")]
        [SerializeField] private List<GameObject> pointList; //Points are the areas notes can exist. The centre point is where players hit the note.
        [SerializeField] private List<NoteBehaviour> notesInPlay;
        [SerializeField] private GameObject notesHolder; //Just for organisation in the inspector

        //this is temp, incase I dont want it to start on load.
        public void Start()
        {
            Initialize();
        }
        public void Initialize(/*SongData _songData*/)
        {
            notesToPlay = new List<GameObject>(songData.noteList);
            startingBPM = songData.startingBPM;

            BPM = startingBPM; //I made BPM different from startingBPM so that BPM could change freely while keeping track of what it started at
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) { MoveNotes(); }

            BPS = BPM / 60; //This convers BPM to seconds, and will continue to update if the beat quickens or slows
            SPB = 1 / BPS; //Converts BeatsPerSecond into SecondsPerBeat
        }

        private void MoveNotes()
        {
            for (int i = notesInPlay.Count - 1; i >= 0; i--)
            {
                NoteBehaviour note = notesInPlay[i];

                if (note.CurrentPoint().IsEnd())
                {
                    notesInPlay.RemoveAt(i);
                    Destroy(note.gameObject);
                }
                else
                {
                    note.MoveNote(SPB);

                    if (note.CurrentPoint().IsCenter()) { note.PassedCentre(); }
                }
            }

            SpawnNextNote();
        }


        private void SpawnNextNote()
        {
            currentNote++;

            GameObject noteToSpawn = Instantiate(notesToPlay[0], pointList[0].transform); //Spawns the note
            noteToSpawn.GetComponent<NoteBehaviour>().Initialize(pointList[0].GetComponent<Point>(), currentNote); //Initialzies the Note

            noteToSpawn.transform.SetParent(notesHolder.transform); //Puts the spawned notes into the holder

            notesInPlay.Add(noteToSpawn.GetComponent<NoteBehaviour>()); //Adds spawned note to NotesInPlay list
            notesToPlay.Remove(notesToPlay[0]); //Removes spawned note from notelist
        }
    }
}

