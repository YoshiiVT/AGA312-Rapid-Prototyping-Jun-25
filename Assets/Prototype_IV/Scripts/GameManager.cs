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
        //Temp
        [SerializeField] private PlayerController playerController;
        
        [SerializeField] private GameObject deathPanel;

        [SerializeField] private GameState gameState; 

        [SerializeField, ReadOnly] private int points;
        [SerializeField] private TMP_Text pointText;

        public void Start()
        {
            UpdatePoints();
            deathPanel.SetActive(false);
        }

        public void addpoint()
        {
            points++;
            print(points);
            UpdatePoints();
        }

        public void Death()
        {
            deathPanel.SetActive(true);
            Time.timeScale = 0;
        }

        public void Revive()
        {
            Time.timeScale = 1;
            deathPanel.SetActive(false);
            playerController.DestroyLastCollidedObj();
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

