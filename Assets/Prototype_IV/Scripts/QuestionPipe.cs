using UnityEngine;
using PROTOTYPE_4;
using TMPro;

public class QuestionPipe : GameBehaviour
{
    [Header("Managers")]
    //Temp
    [SerializeField, ReadOnly] private GameManager gameManager;
    [SerializeField, ReadOnly] private EquationGenerator equationGenerator;

    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text correctAnswer;
    [SerializeField] private TMP_Text falseAnswer;

    

    public void Start()
    {
        //Temp
        GameObject gameManagerobj = GameObject.Find("GameManager");
        gameManager = gameManagerobj.GetComponent<GameManager>();
        if (gameManager == null) { Debug.LogError("GAMEMANAGER NOT FOUND!!!"); }

        GameObject equationGeneratorobj = GameObject.Find("EquationGenerator");
        equationGenerator = equationGeneratorobj.GetComponent<EquationGenerator>();
        if (equationGenerator == null) { Debug.LogError("EQUATION GENERATOR NOT FOUND!!!"); }

        equationGenerator.GenerateEquation();

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

        correctAnswer.text = equationGenerator.correctAnswer.ToString();
        falseAnswer.text = equationGenerator.dummyAnswers[0].ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            gameManager.addpoint();
        }
    }
}
