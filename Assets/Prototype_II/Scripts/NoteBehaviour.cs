using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class NoteBehaviour : GameBehaviour<NoteBehaviour>
{
    [SerializeField] public List<GameObject> notes;

    [SerializeField] private GameObject note; //This is what the player will hit
    [SerializeField] private GameObject beat; //This is what the player will ignore

    [SerializeField] private float beatTempo; //This can be changed for speed and difficulty. Adverage is 120bpm

    [SerializeField] private bool isManual;

    [SerializeField] private Column startColumn;

    [SerializeField] private Canvas noteArea;

    [SerializeField] private TMP_Text tempText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isManual) 
        { 
            if (Input.GetKeyDown(KeyCode.Q)) { TempMoveNote(); /*Debug.Log("Moving Notes");*/ }
            if (Input.GetKeyDown(KeyCode.E)) { SpawnNote(); Debug.Log("Spawning Note"); tempText.text = "Spawning Note"; }
        }
    }

    private void SpawnNote()
    {
        GameObject noteToSpawn = Instantiate(note, startColumn.transform.position, Quaternion.identity);

        // Set the parent to the noteArea Canvas (without changing world position)
        noteToSpawn.transform.SetParent(noteArea.transform, worldPositionStays: false);

        noteToSpawn.GetComponent<Note>().Initialize(startColumn);

        notes.Add(noteToSpawn);
    }

    private void TempMoveNote()
    {
        Debug.Log("Moving Notes");
        for (int i = 0; i < notes.Count; i++)
        {
            tempText.text = "Spawning Note " + i;
            Debug.Log("Moving Note " + i);
            GameObject noteToMove = notes[i];
            noteToMove.GetComponent<Note>().MoveNote(2/*beatTempo * Time.deltaTime*/);
        }
    }
    
}
