using UnityEngine;
using TMPro;

namespace PROTOTYPE_2
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int currentScore;
        [SerializeField] private int scorePerNote;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text multiplierText;

        public void Start()
        {
            multiplierText.text = "Markiplier : x1";
            scoreText.text = "Score : " + currentScore;
        }

        public void NoteHit()
        {
            Debug.Log("Hit On Time");

            currentScore += scorePerNote;
            scoreText.text = "Score : " + currentScore;
        }
        public void NoteMissed()
        {
            Debug.Log("Missed Note");
        }
    }
}

