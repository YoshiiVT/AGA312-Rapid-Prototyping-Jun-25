using DG.Tweening;
using UnityEngine;

namespace PROTOTYPE_2
{
    public class BeatBehaviour : GameBehaviour
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
            if (currentColumn.IsEnd() == true) { tempManager.notes.Remove(gameObject); Destroy(gameObject); } //Put failed logic here...
        }
    }

    [System.Serializable]
    public class Beat 
    {
        [SerializeField] private bool playerBeat; //If True, then it is a beat that the player has to hit
        [SerializeField] private bool speedUpBPM; //If True, then the BPM is increased for the next beat
        [SerializeField] private bool speedDownBPM; //If True, then the BPM is decreased for the next beat

        [SerializeField] private int beatOrder; //This shows how many beats came before it.

        private BeatData beatData;
        [SerializeField] private BeatBehaviour beatBehaviour;

        public void Initialize(BeatData _beatData, int _beatOrder)
        {
            beatData = _beatData;
            playerBeat = _beatData.playerBeat;
            speedUpBPM = _beatData.speedUpBPM;
            speedDownBPM = _beatData.speedDownBPM;
            beatOrder = _beatOrder;
        }
    }
}
