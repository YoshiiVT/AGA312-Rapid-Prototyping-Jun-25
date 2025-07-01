using TMPro;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardValue;
    [SerializeField] private TMP_Text cardDescription;
    [SerializeField] private TMP_Text cardSuite;
    [SerializeField] private SpriteRenderer cardIcon;
    [SerializeField] private Renderer cardRenderer;

    private CardData cardData;

    public void Initialize(CardData _cardData)
    {
        cardData = _cardData;
        cardName.text = _cardData.cardID.ToString();
        cardValue.text = _cardData.value.ToString();
        cardDescription.text = cardData.description.ToString();
        cardSuite.text = _cardData.suite.ToString();
        cardIcon.sprite = _cardData.icon;
        cardRenderer.material.color = _cardData.colour;

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce);
    }
}
