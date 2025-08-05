using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Revive : MonoBehaviour
{
    [SerializeField] private Button buttonA;
    [SerializeField] private Button buttonB;

    [SerializeField] private EquationGenerator equationGenerator;

    [Header("Panel Assets")]
    [SerializeField] private TMP_Text question;
 
    private void Awake()
    {
        if (equationGenerator == null) { Debug.LogError("EquationGenerater NOT Found!"); }
    }

    public void GenerateRevivalQuestion()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        buttonA.onClick.RemoveAllListeners();
        buttonB.onClick.RemoveAllListeners();

        equationGenerator.GenerateEquation();

        question.text = equationGenerator.numberOne.ToString();


    }
}
