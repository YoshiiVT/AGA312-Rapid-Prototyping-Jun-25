using UnityEngine;
using TMPro;

namespace PROTOTYPE_2
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int currentScore;
        [SerializeField] private int scorePerNote;

        [SerializeField, ReadOnly] private int currentMultiplier;
        [SerializeField, ReadOnly] private int multiplierTracker;
        [SerializeField] private int[] multiplierThresholds;

        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text multiplierText;

        public void Start()
        {
            currentMultiplier = 1;

            multiplierText.text = "Markiplier : x" + currentMultiplier;
            scoreText.text = "Score : " + currentScore;
        }

        public void NoteHit()
        {
            Debug.Log("Hit On Time");

            if (currentMultiplier - 1 < multiplierThresholds.Length)
            {
                multiplierTracker++;

                if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
                {
                    multiplierTracker = 0;
                    currentMultiplier++;
                }
            }

            currentScore += scorePerNote * currentMultiplier;

            scoreText.text = "Score : " + currentScore;
            multiplierText.text = "Markiplier : x" + currentMultiplier;
        }
        public void NoteMissed()
        {
            Debug.Log("Missed Note");

            currentScore -= (scorePerNote / 2) * currentMultiplier;
            if (currentScore <= 0) { currentScore = 0; }
            currentMultiplier = 1;
            multiplierTracker = 0;

            scoreText.text = "Score : " + currentScore;
            multiplierText.text = "Markiplier : x" + currentMultiplier;

        }
    }
}

