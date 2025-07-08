using UnityEngine;

namespace PROTOTYPE_2
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int currentScore;
        [SerializeField, ReadOnly] private int scorePerNote;

        public void NoteHit()
        {
            Debug.Log("Hit On Time");
        }
        public void NoteMissed()
        {
            Debug.Log("Missed Note");
        }
    }
}

