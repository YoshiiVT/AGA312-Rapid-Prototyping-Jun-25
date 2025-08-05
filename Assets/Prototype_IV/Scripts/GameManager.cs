using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace PROTOTYPE_4
{
    public enum GameState
    {
        PLAYING,
        DEATH,
        GAMEOVER
    }

    public class GameManager : GameBehaviour
    {
        //Temp
        [SerializeField] private PlayerController playerController;

        [SerializeField] private GameState gameState;

        [Header("Screens")]
        [SerializeField] private GameObject deathPanel;
        [SerializeField] private GameObject revivePanel;
        [SerializeField] private GameObject gameOverPanel;

        [Header("revivePanel")]
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private TMP_Text answerText;
        [SerializeField] private TMP_Text fakeText;

        [Header("GameOverPanel")]
        [SerializeField] private TMP_Text finalScoreText;

        [SerializeField] private Button buttonA;
        [SerializeField] private Button buttonB;

        [SerializeField, ReadOnly] private int points;
        [SerializeField] private TMP_Text pointText;

        public void Start()
        { 
            UpdatePoints();
            deathPanel.SetActive(false);
            revivePanel.SetActive(false);
            gameOverPanel.SetActive(false);
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

        public void Math()
        {
            deathPanel.SetActive(false);
            revivePanel.SetActive(true);

            int number1 = GenerateRandomNumber();
            int number2 = GenerateRandomNumber();

            int correctAnswer = number1 + number2;

            questionText.text = number1 + " + " + number2 + " = ? ";
            answerText.text = correctAnswer.ToString();

            int dummy;
            do
            {
                dummy = Random.Range(correctAnswer - 10, correctAnswer + 10);
            }
            while (dummy == correctAnswer);
            fakeText.text = dummy.ToString();

        }

        public void Revive()
        {
            Time.timeScale = 1;
            revivePanel.SetActive(false);
            playerController.DestroyLastCollidedObj();
        }

        public void GameOver()
        {
            deathPanel.SetActive(false);
            revivePanel.SetActive(false);
            gameOverPanel.SetActive(true);

            finalScoreText.text = points.ToString();
        }

        private void UpdatePoints()
        {
            pointText.text = "Score : " + points;
        }

        public GameState CurrentGameState()
        {
            return gameState;
            Button.ButtonClickedEvent()
        }

        private int GenerateRandomNumber()
        {
            int number = Random.Range(0, 10);
            return number;
        }
    }
}

