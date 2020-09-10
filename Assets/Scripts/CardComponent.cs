using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour
{
    public Card card;
    public string cardname;
    private Text text;
    private bool isUsed;//사용되면 true, 아니면 false
    private Image image;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<Text>();
        image = GetComponent<Image>();
        isUsed = false;
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

    public void OffCard()
    {
        isUsed = true;
        Color color = new Color(125f / 255f, 125f / 255f, 125f / 255f, 125f / 255f);
        image.color = color;
    }

    public void OnCard()
    {
        isUsed = false;
        Color color = new Color(1f, 1f, 1f, 1f);
        image.color = color;
    }

    public bool GetIsUsed() { return isUsed; }
}
