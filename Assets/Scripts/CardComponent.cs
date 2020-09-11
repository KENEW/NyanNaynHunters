using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour
{
    public List<Sprite> m_Backgrounds;
    
    public Card card;
    public string cardname;
    public Text CardName;
    private bool isUsed;//사용되면 true, 아니면 false
    public Image BackgroundImage;

    private void Awake()
    {
        isUsed = false;
    }

    public void SetCard(Card card)
    {
        this.card = card;
        cardname = card.cardName;
        CardName.text = cardname;
    }

    public void DeleteCard()
    {
        card = null;
        cardname = "";
        CardName.text = cardname;
    }

    public void OffCard()
    {
        isUsed = true;
        Color color = new Color(125f / 255f, 125f / 255f, 125f / 255f, 125f / 255f);
        BackgroundImage.color = color;
    }

    public void OnCard()
    {
        isUsed = false;
        Color color = new Color(1f, 1f, 1f, 1f);
        BackgroundImage.color = color;
    }

    public bool GetIsUsed() { return isUsed; }
}
