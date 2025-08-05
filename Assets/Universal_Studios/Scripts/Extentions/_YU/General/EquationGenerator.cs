using System.Collections.Generic;
using UnityEngine;

public class EquationGenerator : GameBehaviour
{
    public enum Difficulty { EASY, MEDIUM, HARD}
    public Difficulty difficulty;
    
    public enum EquationType { ADDITION, SUBTRACTION, MULTIPLICATION, DIVISION }
    public EquationType equationType;

    public int numberOne;
    public int numberTwo;
    public int correctAnswer;
    public List<int> dummyAnswers;

    public YU.Range easyRange;
    public YU.Range mediumRange;
    public YU.Range hardRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) GenerateAddition();
        if (Input.GetKeyDown(KeyCode.S)) GenerateSubtraction();
        if (Input.GetKeyDown(KeyCode.M)) GenerateMultiplication();
        if (Input.GetKeyDown(KeyCode.D)) GeneracteDivision();
    }

    public void GenerateEquation()
    {
        int random = 0;

        switch (difficulty)
        {
            case Difficulty.EASY:
                // Higher chance of Addition and Subtraction
                random = Random.Range(0, 10); // 0–9
                if (random < 4)
                    GenerateAddition();       // 40%
                else if (random < 8)
                    GenerateSubtraction();    // 40%
                else if (random == 8)
                    GenerateMultiplication(); // 10%
                else
                    GeneracteDivision();      // 10%
                break;

            case Difficulty.MEDIUM:
                // Equal chance for all 4
                random = Random.Range(0, 4); // 0–3
                switch (random)
                {
                    case 0: GenerateAddition(); break;
                    case 1: GenerateSubtraction(); break;
                    case 2: GenerateMultiplication(); break;
                    case 3: GeneracteDivision(); break;
                }
                break;

            case Difficulty.HARD:
                // Higher chance of Multiplication and Division
                random = Random.Range(0, 10); // 0–9
                if (random < 2)
                    GenerateAddition();       // 20%
                else if (random < 4)
                    GenerateSubtraction();    // 20%
                else if (random < 7)
                    GenerateMultiplication(); // 30%
                else
                    GeneracteDivision();      // 30%
                break;
        }
    }

    public void GenerateAddition()
    {
        equationType = EquationType.ADDITION;
        GenerateRandomNumbers();
        correctAnswer = numberOne + numberTwo;
        Debug.Log(numberOne + " + " + numberTwo + " = " + correctAnswer);
        GenerateDummyAnswers();
    }

    public void GenerateSubtraction()
    {
        equationType = EquationType.SUBTRACTION;
        GenerateRandomNumbers();
        correctAnswer = numberOne - numberTwo;
        Debug.Log(numberOne + " - " + numberTwo + " = " + correctAnswer);
        GenerateDummyAnswers();
    }

    public void GenerateMultiplication()
    {
        equationType = EquationType.MULTIPLICATION;
        GenerateRandomNumbers();
        correctAnswer = numberOne * numberTwo;
        Debug.Log(numberOne + " x " + numberTwo + " = " + correctAnswer);
        GenerateDummyAnswers();
    }

    public void GeneracteDivision()
    {
        equationType = EquationType.DIVISION;
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
