using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardComponent : MonoBehaviour
{
    public Card card;
    public string cardname;

    public void SetCard(Card card)
    {
        this.card = card;
        cardname = card.cardName;
    }

    public void DeleteCard()
    {
        card = null;
        cardname = "";
    }
}
