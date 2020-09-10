using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private CardComponent card_c;
    private CardField cardField;
    public static bool canClick;

    public enum CardImageType
    {
        Handler,
        Field
    };
    public CardImageType imageType;
    

    void Awake()
    {
        card_c = GetComponent<CardComponent>();
        cardField = FindObjectOfType<CardField>();
        canClick = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (imageType == CardImageType.Field)
        {
            if (canClick == false) return;
            if (cardField.playerHandler.Count >= 3 || cardField.cardList.Count > 12) return;
            Card card = card_c.card;
            cardField.playerHandler.Enqueue(card);
            cardField.cardList.Remove(card);
            card_c.DeleteCard();

            GameManager.Instance.clickedCardCount += 1;
            //while (true)
            //{
            //    Card card2 = CardManager.Instance.GetRandomCard();
            //    if (CardManager.Instance.MaxCountCheck(card2, cardField.cardList))
            //    {
            //        cardField.AddCard(card2);
            //        break;
            //    }
            //}

            cardField.UpdateCardPos();
            cardField.UpdateHandler();
        }
        else
        {
            if (card_c.card == null) return;
            Card card = card_c.card;
            cardField.AddCard(card);
            cardField.UpdateCardPos();

            cardField.playerHandler.Dequeue();
            card_c.DeleteCard();

            GameManager.Instance.clickedCardCount -= 1;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {


    }


}
