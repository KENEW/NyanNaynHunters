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
        if (cardField.playerHandler.Count > 3) return;
        Card card = card_c.card;
        //PlayerManager.Instance.Player.handler.Enqueue(card);
        cardField.playerHandler.Enqueue(card);
        cardField.cardList.Remove(card);
        cardField.UpdateCardPos();
        cardField.UpdateHandler();
        card_c.DeleteCard();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        while (true)
        {
            Card card = CardManager.Instance.GetRandomCard();
            if (CardManager.Instance.MaxCountCheck(card, cardField.cardList))
            {
                cardField.AddCard(card);
                cardField.UpdateCardPos();
                break;
            }
        }
        
    }

    
}
