using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardField : MonoBehaviour
{
    public RectTransform[] cardPos;
    public float width;//폭
    public float height;//너비
    public List<Card> list;
    public RectTransform[] handlerPos;
    public Queue<Card> playerHandler;


    private void Awake()
    {
        list = new List<Card>();
        playerHandler = new Queue<Card>();
    }

    private void Start()
    {
        SetCard();
    }

    public void UpdateCardPos()
    {
        for(int i=0; i<list.Count; i++)
        {
            cardPos[i].GetComponent<CardComponent>().SetCard(list[i]);
        }
    }

    public void UpdateHandler()
    {
        Queue<Card> t = new Queue<Card>(playerHandler); //clone
        for(int i=0;i<playerHandler.Count; i++)
        {
            handlerPos[i].GetComponent<CardComponent>().SetCard(t.Dequeue());
        }
    }

    void SetCard()
    {
        int startCount = CardSetting.AttackCardStartCount;
        while (startCount > 0)
        {
            int random = Random.Range(0, TBL_ATTACK_CARD.CountEntities);
            AttackCard card = new AttackCard(TBL_ATTACK_CARD.GetEntity(random));
            if (!CheckDuplicate(card))
            {
                list.Add(card);
                startCount--;
            }
        }

        startCount = CardSetting.EnergyCardStartCount;
        while (startCount > 0)
        {
            int random = Random.Range(0, TBL_ENERGY_CARD.CountEntities);
            EnergyCard card = new EnergyCard(TBL_ENERGY_CARD.GetEntity(random));
            if (!CheckDuplicate(card))
            {
                list.Add(card);
                startCount--;
            }
        }

        startCount = CardSetting.GuardCardStartCount;
        while (startCount > 0)
        {
            int random = Random.Range(0, TBL_GUARD_CARD.CountEntities);
            GuardCard card = new GuardCard(TBL_GUARD_CARD.GetEntity(random));
            if (!CheckDuplicate(card))
            {
                list.Add(card);
                startCount--;
            }
        }

        startCount = CardSetting.HealCardStartCount;
        while (startCount > 0)
        {
            int random = Random.Range(0, TBL_HEAL_CARD.CountEntities);
            HealCard card = new HealCard(TBL_HEAL_CARD.GetEntity(random));
            if (!CheckDuplicate(card))
            {
                list.Add(card);
                startCount--;
            }
        }

        startCount = CardSetting.MoveCardStartCount;
        while (startCount > 0)
        {
            int random = Random.Range(0, TBL_MOVE_CARD.CountEntities);
            MoveCard card = new MoveCard(TBL_MOVE_CARD.GetEntity(random));
            if (!CheckDuplicate(card))
            {
                list.Add(card);
                startCount--;
            }
        }
        
        

        Shuffle(list);

        //for(int i=0; i<list.Count; i++)
        //{
        //    cardPos[i].GetComponent<CardComponent>().SetCard(list[i]);
        //}

        UpdateCardPos();
    }


    private bool CheckDuplicate(Card target)
    {
        foreach(Card card in list)
        {
            if (card.cardName.Equals(target.cardName)) return true;
        }
        return false;
    }

    private void Shuffle<T>(IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
