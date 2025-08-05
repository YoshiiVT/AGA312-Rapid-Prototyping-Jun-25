using UnityEngine;
using UnityEngine.UI;

public class Revive : MonoBehaviour
{
    [SerializeField] private Button buttonA;
    [SerializeField] private Button buttonB;

    public void GenerateRevivalQuestion()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        buttonA.onClick.RemoveAllListeners();
        buttonB.onClick.RemoveAllListeners();
    }
}
