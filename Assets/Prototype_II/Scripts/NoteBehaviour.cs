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

        [Header("Note References")]
        [SerializeField] private NoteBehaviour noteBehaviour;

        [Header("Note Properties")]
        [SerializeField] private bool playerBeat; //If True, then it is a beat that the player has to hit
        [SerializeField] private bool speedUpBPM; //If True, then the BPM is increased for the next beat
        [SerializeField] private bool speedDownBPM; //If True, then the BPM is decreased for the next beat

        [Header("Note Sprites")]
        [SerializeField] private SpriteRenderer noteSprite; //This is the sprite that all beats have.
        [SerializeField] private SpriteRenderer enemySprite; //Is the sprite of the player beat, it is ontop of the regular beat.
        [SerializeField] private SpriteRenderer splatSprite; //This is the sprite used for the paint splat when a note is hit.

        [Header("Note Order")]
        [SerializeField] private int noteOrder; //This shows how many notes came before it.

        [Header("Note Variables")]
        [SerializeField, ReadOnly] private bool passedCenter;

        [Header("Point References")]
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

            noteSprite.sortingOrder = 0;
            if (enemySprite != null) { enemySprite.sortingOrder = 1; }
            splatSprite.sortingOrder = 2;
        }

        public void MoveNote(float _SPB)
        {
            Point nextKey = currentPoint.GetNextKey();

            transform.DOMoveX(nextKey.transform.position.x, _SPB);

            currentPoint = nextKey;

            if (!passedCenter)
            {
                ChangeSortingValues(3);
            }
            else ChangeSortingValues(-3);
        }

        private void ChangeSortingValues(int i)
        {
            noteSprite.sortingOrder += i;
            if (enemySprite != null) { enemySprite.sortingOrder += i; }
            splatSprite.sortingOrder += i;
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

        /// <summary>
        /// This returns the noteOrder when checked.
        /// </summary>
        /// <returns></returns>
        public int GetNoteOrder()
        {
            return noteOrder;
        }

        /// <summary>
        /// This returns the currentPoint when checked.
        /// </summary>
        /// <returns></returns>
        public Point CurrentPoint()
        {
            return currentPoint;
        }

        /// <summary>
        /// This toggles the bool "Passed Center
        /// </summary>
        public void PassedCentre() 
        {
            passedCenter = true;
        }
    }
}
