using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;

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

        [Header("DeathPanel")]
        [SerializeField] private int reviveOppertunityCountdown;
        [SerializeField] private Image reviveOppertunityImage;
        [SerializeField, ReadOnly] private bool reviving; 

        [Header("revivePanel")]
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private Button buttonA;
        [SerializeField] private Button buttonB;
        [SerializeField] private Image mathTimerBar;
        [SerializeField, ReadOnly] private bool mathing;

        [Header("GameOverPanel")]
        [SerializeField] private TMP_Text finalScoreText;

        [Header("Points")]
        [SerializeField, ReadOnly] private int points;
        [SerializeField] private TMP_Text pointText;

        [Header("GameState")]
        [SerializeField] private GameState gameState;
        public float speed;
        public float obstacleLifespan;
        public float countdown;
        [SerializeField, ReadOnly] private int mathCounter;
        

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

        public async void Death()
        {
            deathPanel.SetActive(true);
            Time.timeScale = 0;

            await CountdownASY.CountdownWithBar(countdown, reviveOppertunityImage, () =>
            {
                if (reviving) { reviving = false; return; }
                GameOver();
            });
        }

        public async void ReviveMath()
        {
            reviving = true;

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

            await CountdownASY.CountdownWithBar(countdown, mathTimerBar, () =>
            {
                if (mathing) { mathing = false; return; }
                GameOver();
            });
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
            mathing = true;
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

            countdown -= 0.25f;
        }
    }
}

