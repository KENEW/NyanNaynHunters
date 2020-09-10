using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private CardComponent card_c;
    private CardField cardField;

    void Awake()
    {
        card_c = GetComponent<CardComponent>();
        cardField = FindObjectOfType<CardField>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (cardField.playerHandler.Count >= 3 || cardField.cardList.Count > 12) return;
        Card card = card_c.card;
        cardField.playerHandler.Enqueue(card);
        cardField.cardList.Remove(card);
        card_c.DeleteCard();

        while (true)
        {
            Card card2 = CardManager.Instance.GetRandomCard();
            if (CardManager.Instance.MaxCountCheck(card2, cardField.cardList))
            {
                cardField.AddCard(card2);
                break;
            }
        }

        cardField.UpdateCardPos();
        cardField.UpdateHandler();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (cardField.playerHandler.Count + 1 >= 3 || cardField.cardList.Count >= 12) return;

    }


}
