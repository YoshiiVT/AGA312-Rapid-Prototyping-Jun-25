using UnityEngine;
using DG.Tweening;

public class NoteBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject note;

    [SerializeField] private Column startColumn;
    [SerializeField, ReadOnly] private Column currentColumn;
    [SerializeField] private Column endColumn;

    [SerializeField] private float moveTweenTime = 1f; //This can be changed for difficulty
    [SerializeField] private Ease moveEase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        note.transform.position = startColumn.transform.position;
        currentColumn = startColumn;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { MoveNote(); Debug.Log("Moving Note"); }
    }

    private void MoveNote()
    {
        /*
        Column column = currentColumn.GetComponent<Column>();
        if ( column == null) { Debug.LogError("Current Column does NOT have Column script."); }
        */

        Column nextColumn = currentColumn.GetNextColumn(); Debug.Log("Found Next Coloumn: " + nextColumn);
        note.transform.DOMoveX(nextColumn.transform.position.x, moveTweenTime);//note.transform.position = nextColumn.transform.position;
        currentColumn = nextColumn;
    }
}
