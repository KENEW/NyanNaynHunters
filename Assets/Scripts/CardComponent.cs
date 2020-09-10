using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour
{
    public Card card;
    public string cardname;
    private Text text;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<Text>();
    }

    public void SetCard(Card card)
    {
        this.card = card;
        cardname = card.cardName;
        text.text = cardname;
    }

    public void DeleteCard()
    {
        card = null;
        cardname = "";
        text.text = cardname;
    }
}
