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
        Card card = card_c.card;
        //PlayerManager.Instance.Player.handler.Enqueue(card);
        cardField.playerHandler.Enqueue(card);
        cardField.list.Remove(card);
        cardField.UpdateCardPos();
        cardField.UpdateHandler();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerManager.Instance.Player.UseCard(cardField.playerHandler.Dequeue());
    }

    
}
