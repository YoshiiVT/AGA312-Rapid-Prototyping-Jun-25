using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;

    private void Awake()
    {
        if (promptText == null) { Debug.LogError("Prompt Text returned NULL"); }
    }

    public void UpdateText(string _promptMessage)
    {
        promptText.text = _promptMessage;
    }
}
