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
        }

        private void MoveNotes()
        {
            foreach (NoteBehaviour note in notesInPlay)
            {
                if (note.CurrentPoint().IsEnd()) { notesInPlay.Remove(note); Destroy(note.gameObject); } //If note is at the end point it passes.

                note.MoveNote(BPM);
            }

            SpawnNextNote();

            //Find all notes in play. 
            //Check what point they are currently on
            //IF on last note then activate it as a miss
            //Move all notes to the next point
            //Allocate that as their current point
            //Spawn new note
        }

        private void SpawnNextNote()
        {
            currentNote++;

            GameObject noteToSpawn = Instantiate(notesToPlay[0], pointList[0].transform); //Spawns the note
            noteToSpawn.GetComponent<NoteBehaviour>().Initialize(pointList[0].GetComponent<Point>(), currentNote); //Initialzies the Note

            notesInPlay.Add(noteToSpawn.GetComponent<NoteBehaviour>()); //Adds spawned note to NotesInPlay list
            notesToPlay.Remove(notesToPlay[0]); //Removes spawned note from notelist
        }
    }
}

