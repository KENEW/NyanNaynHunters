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

    public Image CardImage;


    [Header("무브 이미지")] public Sprite m_MoveSprite;
    [Header("어택 이미지")] public Sprite m_AttackSprite;
    [Header("가드 이미지")] public Sprite m_GuardSprite;
    [Header("힐 이미지")] public Sprite m_HealSprite;
    [Header("에너지 이미지")] public Sprite m_EnergySprite;
    
    private void Awake()
    {
        isUsed = false;
    }

    public void SetCard(Card card)
    {
        this.card = card;
        cardname = card.cardName;
        CardName.text = cardname;

        //CardImage.enabled = true;
      
        /**
        if (card is MoveCard)
        {
            CardImage.sprite = m_MoveSprite;
        }

        if (card is AttackCard)
        {
            CardImage.sprite = m_AttackSprite;
        }

        if (card is GuardCard)
        {
            CardImage.sprite = m_GuardSprite;
        }

        if (card is HealCard)
        {
            CardImage.sprite = m_HealSprite;
        }

        if (card is EnergyCard)
        {
            CardImage.sprite = m_EnergySprite;
        }

        if (card is EmptyCard)
        {
            CardImage.enabled = false;
        }
        **/
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
