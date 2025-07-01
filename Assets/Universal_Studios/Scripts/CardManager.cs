using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CardManager : MonoBehaviour
{
    public List<CardData> cardData;

    public GameObject cardPrefab;

    public int handCound = 5;

    public CardData GetCard(int _cardID) => cardData.Find(x => x.cardID == _cardID);

    public List<GameObject> cardsInHand;

    private IEnumerator BuildDeck()
    {
        ListX.DestroyList(cardsInHand);
        ListX.ShuffleList(cardData);

        for(int i=0; i < handCound; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(i * 3, 5, 0), transform.rotation);
            newCard.GetComponent<Card>().Initialize(cardData[i]);
            cardsInHand.Add(newCard);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) { StartCoroutine(BuildDeck()); }
    }
}
