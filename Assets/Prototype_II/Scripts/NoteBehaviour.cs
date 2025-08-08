using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace PROTOTYPE_2
{
    public class NoteBehaviour : GameBehaviour
    {
        [Header("Managers")]
        [SerializeField] private SongPlayer _SongPlayer;
        [SerializeField] private GameManager _GameManager;

        [Header("Beat Properties")]
        [SerializeField] private bool playerBeat; //If True, then it is a beat that the player has to hit
        [SerializeField] private bool speedUpBPM; //If True, then the BPM is increased for the next beat
        [SerializeField] private bool speedDownBPM; //If True, then the BPM is decreased for the next beat

        [Header("Beat Order")]
        [SerializeField] private int noteOrder; //This shows how many notes came before it.

        [Header("Beat References")]
        [SerializeField] private NoteBehaviour noteBehaviour;

        [Header("Key References")]
        [SerializeField, ReadOnly] private Point currentPoint; //This is the point the note is currently on, changes after every move.

        [Header("Tweening")]
        [SerializeField] private Ease moveEase;

        public void Initialize(Point startPoint, int _noteOrder)
        {
            #region Managers
            _SongPlayer = GameObject.Find("SongPlayer").GetComponent<SongPlayer>();

            if (_SongPlayer == null) { Debug.LogError("SONGPLAYER NOT FOUND!!!"); }

            //_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            //if (_GameManager == null) { Debug.LogError("GAMEMANAGER NOT FOUND!!!"); }
            #endregion

            transform.position = startPoint.transform.position;
            currentPoint = startPoint;
            noteOrder = _noteOrder;
        }

        public void MoveNote(float _SPB)
        {
            Point nextKey = currentPoint.GetNextKey();

            transform.DOMoveX(nextKey.transform.position.x, _SPB);

            currentPoint = nextKey;
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
            return noteOrder;
        }

        public Point CurrentPoint()
        {
            return currentPoint;
        }
    }
}
