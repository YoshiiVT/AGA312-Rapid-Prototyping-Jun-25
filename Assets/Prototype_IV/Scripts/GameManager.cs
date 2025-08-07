using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

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

        [Header("GameState")]
        [SerializeField] private GameState gameState;
        public float speed;
        public float obstacleLifespan;
        [SerializeField, ReadOnly] int mathCounter;

        public void Start()
        { 
            UpdatePoints();
            deathPanel.SetActive(false);
            revivePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            StartCoroutine(SpeedGrowth());
        }

        private IEnumerator SpeedGrowth()
        {
            if (speed <= 10) { speed += 0.005f; }
            else { speed += 0.001f; }
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SpeedGrowth());
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

        public void ReviveMath()
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

            UsedMath();
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

        private void GameOver()
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

        private void UsedMath()
        {
            mathCounter++;

            switch (mathCounter)
            {
                case <=2:
                    equationGenerator.difficulty = EquationGenerator.Difficulty.EASY;
                    break;
                case <=5:
                    equationGenerator.difficulty = EquationGenerator.Difficulty.MEDIUM;
                    break;
                case >= 8:
                    equationGenerator.difficulty = EquationGenerator.Difficulty.HARD;
                    break;

            }
        }
    }
}

