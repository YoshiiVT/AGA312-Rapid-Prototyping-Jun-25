using System.Collections.Generic;
using UnityEngine;

public class EquationGenerator : MonoBehaviour
{
    public enum Difficulty { EASY, MEDIUM, HARD}
    public Difficulty difficulty;
    
    public int numberOne;
    public int numberTwo;
    public int correctAnswer;
    public List<int> dummyAnswers;

    public BV.Range easyRange;
    public BV.Range mediumRange;
    public BV.Range hardRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) GenerateAddition();
        if (Input.GetKeyDown(KeyCode.S)) GenerateSubtraction();
        if (Input.GetKeyDown(KeyCode.M)) GenerateMultiplication();
        if (Input.GetKeyDown(KeyCode.D)) GeneracteDivision();
    }

    public void GenerateAddition()
    {
        GenerateRandomNumbers();
        correctAnswer = numberOne + numberTwo;
        Debug.Log(numberOne + " + " + numberTwo + " = " + correctAnswer);
        GenerateDummyAnswers();
    }

    public void GenerateSubtraction()
    {
        GenerateRandomNumbers();
        correctAnswer = numberOne - numberTwo;
        Debug.Log(numberOne + " - " + numberTwo + " = " + correctAnswer);
        GenerateDummyAnswers();
    }

    public void GenerateMultiplication()
    {
        GenerateRandomNumbers();
        correctAnswer = numberOne * numberTwo;
        Debug.Log(numberOne + " x " + numberTwo + " = " + correctAnswer);
        GenerateDummyAnswers();
    }

    public void GeneracteDivision()
    {
        GenerateRandomNumbers();
        float tempAnswer = numberOne / numberTwo;
        correctAnswer = Mathf.RoundToInt(tempAnswer);
        Debug.Log(numberOne + " / " + numberTwo + " = " + correctAnswer);
        GenerateDummyAnswers();

    }

    private void GenerateRandomNumbers()
    {
        numberOne = GetRandomNumber();
        numberTwo = GetRandomNumber();
    }

    private int GetRandomNumber()
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                return (int)Random.Range(easyRange.min, easyRange.max);
            case Difficulty.MEDIUM:
                return (int)Random.Range(mediumRange.min, mediumRange.max);
            case Difficulty.HARD:
                return (int)Random.Range(hardRange.min, hardRange.max);
            default:
                return (int)Random.Range(0, 10);
        }
    }

    private void GenerateDummyAnswers()
    {
        {
            for (int i = 0; i < dummyAnswers.Count; i++)
            {
                int dummy;
                do
                {
                    dummy = Random.Range(correctAnswer - 10, correctAnswer + 10);
                }
                while (dummy == correctAnswer || dummyAnswers.Contains(dummy));
                dummyAnswers[i] = dummy;
                Debug.Log("Dummy Answer " + dummy);
            }
        }
    }
}
