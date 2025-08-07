using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace PROTOTYPE_2
{
    public class BeatBehaviour : GameBehaviour
    {
        [Header("Managers")]
        [SerializeField] private SongPlayer _SongPlayer;
        [SerializeField] private GameManager _GameManager;

        [Header("Beat Properties")]
        [SerializeField] private bool playerBeat; //If True, then it is a beat that the player has to hit
        [SerializeField] private bool speedUpBPM; //If True, then the BPM is increased for the next beat
        [SerializeField] private bool speedDownBPM; //If True, then the BPM is decreased for the next beat

        [Header("Beat Order")]
        [SerializeField] private int beatOrder; //This shows how many beats came before it.

        [Header("Beat References")]
        [SerializeField] private BeatBehaviour beatBehaviour;

        [Header("Key References")]
        [SerializeField, ReadOnly] private Key currentColumn;
        [SerializeField] private Key endColumn;

        [Header("Beat Variables")]
        [SerializeField, ReadOnly] bool isDead = false;

        [Header("Tweening")]
        [SerializeField] private Ease moveEase;

        public void Initialize(Key startColumn, BeatData _beatData, int _beatOrder)
        {
            #region Managers
            _SongPlayer = GameObject.Find("SongPlayer").GetComponent<SongPlayer>();

            if (_SongPlayer == null) { Debug.LogWarning("SONGPLAYER NOT FOUND!!!"); }

            _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (_GameManager == null) { Debug.LogWarning("GAMEMANAGER NOT FOUND!!!"); }
            #endregion

            transform.position = startColumn.transform.position;
            currentColumn = startColumn;
            beatOrder = _beatOrder;
        }

        public void MoveNote(float _BPM)
        {
            if (isDead) return;
            Key nextColumn = currentColumn.GetNextKey(); //Debug.Log("Found Next Coloumn: " + nextColumn);
            if (nextColumn.IsStart() == true) { transform.position = nextColumn.transform.position; }
            else { transform.DOMoveX(nextColumn.transform.position.x, _BPM); }
            currentColumn = nextColumn;
            if (currentColumn.IsEnd() == true) { isDead = true; StartCoroutine(NoteDeath(_BPM)); if (playerBeat) { _GameManager.NoteMissed(); } }
        }

        private IEnumerator NoteDeath(float _BPM)
        {

            yield return new WaitForSeconds(_BPM);
            _SongPlayer.beatsInPlay.Remove(gameObject); Destroy(gameObject);
        }

        public bool IsPlayerBeat()
        {
            //Debug.Log("Checking if beat is playable");
            if (playerBeat == true) return true;
            else return false;
        }

        public bool IsSpeedUp()
        {
            //Debug.Log("Checking if beat speeds up");
            if (speedUpBPM == true) return true;
            else return false;
        }

        public bool IsSpeedDown()
        {
            //Debug.Log("Checking if beat speeds down");
            if (speedDownBPM == true) return true;
            else return false;
        }

        public int GetBeatOrder()
        {
            return beatOrder;
        }
    }
}
