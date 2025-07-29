using UnityEngine;
using TMPro;

namespace PROTOTYPE_4
{
    public enum GameState
    {
        PLAYING,
        PAUSED,
        DEATH,
    }

    public class GameManager : GameBehaviour
    {
        
        [SerializeField] private GameState gameState; 

        [SerializeField, ReadOnly] private int points;
        [SerializeField] private TMP_Text pointText;

        public void Start()
        {
            UpdatePoints();
        }

        public void addpoint()
        {
            points++;
            print(points);
            UpdatePoints();
        }

        private void UpdatePoints()
        {
            pointText.text = "Score : " + points;
        }

        public GameState CurrentGameState()
        {
            return gameState;
        }
    }
}

