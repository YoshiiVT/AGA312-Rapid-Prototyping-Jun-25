using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace PROTOTYPE_3
{
    public class GameManager : GameBehaviour<GameManager>
    {
        #region Singleton
        // Singleton Set Up

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("Game Manager already instanced");
                return;
            }
            Instance = this;
        }
        #endregion

        [SerializeField] private TMP_Text countdownText;
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private GameObject gameoverScreen;
        public bool disablePivot = false;

        private void Start()
        {
            StartCoroutine(StartCountdown());
            gameoverScreen.SetActive(false);
        }

        public IEnumerator StartCountdown()
        {
            disablePivot = true;
            countdownText.gameObject.SetActive(false);

            yield return new WaitForSeconds(1);
            countdownText.gameObject.SetActive(true);
            countdownText.text = 3.ToString();

            yield return new WaitForSeconds(1);
            countdownText.text = 2.ToString();

            yield return new WaitForSeconds(1);
            countdownText.text = 1.ToString();

            //Replace with tween later
            yield return new WaitForSeconds(1);
            GameStart();
            disablePivot = false ;
            countdownText.gameObject.SetActive(false);
        }
        private void GameStart()
        {
            _ChM.StartGame();
        }

        public void EndGame()
        {
            EndGameDelay(() =>
            {
                print("Game is Over!!!");
                gameoverScreen.SetActive(true);
                disablePivot = true;
                _CM.SortBowl();
            });
        }

        public void EndGameDelay(Action _onComplete = null)
        {
            Debug.Log("End Game");

            ExecuteAfterSeconds(2, () =>
            {
                _onComplete?.Invoke();
            });
        }

        public void CakeResult(string cake)
        {
            resultText.text = "You made a " + cake + " Cake!!!";
        }
    }

}
