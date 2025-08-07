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
        [Header("Managers")]
        [SerializeField] private PlayerController playerController;
        [SerializeField] private EquationGenerator equationGenerator;

        [SerializeField] private GameState gameState;

        [Header("Screens")]
        [SerializeField] private GameObject deathPanel;
        [SerializeField] private GameObject revivePanel;
        [SerializeField] private GameObject gameOverPanel;

        [Header("revivePanel")]
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private Button buttonA;
        [SerializeField] private Button buttonB;

        [Header("GameOverPanel")]
        [SerializeField] private TMP_Text finalScoreText;

        [Header("Points")]
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

            buttonA.onClick.RemoveAllListeners();
            buttonB.onClick.RemoveAllListeners();

            equationGenerator.GenerateEquation();

            int correctButton = Random.Range(0, 2);

            if (correctButton == 0) 
            {
                ButtonIsRight(buttonA);
                ButtonIsWrong(buttonB);
            }
            else
            {
                ButtonIsRight(buttonB);
                ButtonIsWrong(buttonA);
            }

            switch (equationGenerator.equationType)
            {
                case EquationGenerator.EquationType.ADDITION:
                    questionText.text = equationGenerator.numberOne + " + " + equationGenerator.numberTwo + " =  ? ";
                    break;
                case EquationGenerator.EquationType.SUBTRACTION:
                    questionText.text = equationGenerator.numberOne + " - " + equationGenerator.numberTwo + " =  ? ";
                    break;
                case EquationGenerator.EquationType.MULTIPLICATION:
                    questionText.text = equationGenerator.numberOne + " X " + equationGenerator.numberTwo + " =  ? ";
                    break;
                case EquationGenerator.EquationType.DIVISION:
                    questionText.text = equationGenerator.numberOne + " / " + equationGenerator.numberTwo + " =  ? ";
                    break;
            }
        }

        private void ButtonIsRight(Button button)
        {
            button.onClick.AddListener(() => Revive());
            button.GetComponentInChildren<TMP_Text>().text = equationGenerator.correctAnswer.ToString();
        }

        private void ButtonIsWrong(Button button)
        {
            button.onClick.AddListener(() => GameOver());
            button.GetComponentInChildren<TMP_Text>().text = equationGenerator.dummyAnswers[0].ToString();
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
        }

        private int GenerateRandomNumber()
        {
            int number = Random.Range(0, 10);
            return number;
        }
    }
}

