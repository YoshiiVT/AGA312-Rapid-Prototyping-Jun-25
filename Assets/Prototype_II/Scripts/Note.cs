using DG.Tweening;
using UnityEngine;

namespace PROTOTYPE_2
{
    public class Note : GameBehaviour
    {
        [Header("Coloum References")]
        [SerializeField, ReadOnly] private Column currentColumn;
        [SerializeField] private Column endColumn;

        [SerializeField] private Ease moveEase;

        [SerializeField] private NoteBehaviour tempManager;

        public void Initialize(Column startColumn)
        {
            transform.position = startColumn.transform.position;
            currentColumn = startColumn;
            tempManager = GameObject.Find("NoteBehaviour").GetComponent<NoteBehaviour>();
        }

        public void MoveNote(float _BPM)
        {
            Column nextColumn = currentColumn.GetNextColumn(); Debug.Log("Found Next Coloumn: " + nextColumn);
            if (nextColumn.IsStart() == true) { transform.position = nextColumn.transform.position; }
            else { transform.DOMoveX(nextColumn.transform.position.x, _BPM); }
            currentColumn = nextColumn;
            if (currentColumn.IsEnd() == true) { tempManager.notes.Remove(gameObject); Destroy(gameObject); ; /*Put failed logic here...*/ }
        }
    }
}
