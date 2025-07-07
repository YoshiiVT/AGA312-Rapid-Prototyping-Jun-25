using DG.Tweening;
using System.Collections;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

namespace PROTOTYPE_2
{
    public class BeatBehaviour : GameBehaviour
    {
        [SerializeField] private bool playerBeat; //If True, then it is a beat that the player has to hit
        [SerializeField] private bool speedUpBPM; //If True, then the BPM is increased for the next beat
        [SerializeField] private bool speedDownBPM; //If True, then the BPM is decreased for the next beat

        [SerializeField] private int beatOrder; //This shows how many beats came before it.

        private BeatData beatData;
        [SerializeField] private BeatBehaviour beatBehaviour;

        [SerializeField] private Image panel;

        [SerializeField, ReadOnly] bool isDead = false;

        [Header("Coloum References")]
        [SerializeField, ReadOnly] private Column currentColumn;
        [SerializeField] private Column endColumn;

        [SerializeField] private Ease moveEase;

        [SerializeField] private SongPlayer tempManager;

        
        public void Initialize(Column startColumn, BeatData _beatData, int _beatOrder)
        {
            transform.position = startColumn.transform.position;
            currentColumn = startColumn;
            tempManager = GameObject.Find("SongPlayer").GetComponent<SongPlayer>();

            beatData = _beatData;
            playerBeat = _beatData.playerBeat;
            speedUpBPM = _beatData.speedUpBPM;
            speedDownBPM = _beatData.speedDownBPM;
            beatOrder = _beatOrder;

            panel.color = _beatData.colour;
        }

        public void MoveNote(float _BPM)
        {
            if (isDead) return;
            Column nextColumn = currentColumn.GetNextColumn(); Debug.Log("Found Next Coloumn: " + nextColumn);
            if (nextColumn.IsStart() == true) { transform.position = nextColumn.transform.position; }
            else { transform.DOMoveX(nextColumn.transform.position.x, _BPM); }
            currentColumn = nextColumn;
            if (currentColumn.IsEnd() == true) { isDead = true; StartCoroutine(NoteDeath()); } //Put failed logic here...
        }

        private IEnumerator NoteDeath()
        {

            yield return new WaitForSeconds(1);
            tempManager.beatsInPlay.Remove(gameObject); Destroy(gameObject);
        }

        public bool IsPlayerBeat()
        {
            if (playerBeat) return true;
            else return false;
        }
    }
}
