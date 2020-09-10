using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardField : MonoBehaviour
{
    public RectTransform[] cardPos;
    public float width;//폭
    public float height;//너비
    public List<Card> cardList;
    public RectTransform[] playerHandlerPos;
    public Queue<Card> playerHandler;
    public RectTransform[] enemyHandlerPos;
    public Queue<Card> enemyHandler;


    private void Awake()
    {
        cardList = new List<Card>();
        playerHandler = new Queue<Card>(playerHandlerPos.Length);
        enemyHandler = new Queue<Card>(enemyHandlerPos.Length);

        playerHandler.Enqueue(new MoveCard(TBL_MOVE_CARD.GetEntity(1)));
        playerHandler.Enqueue(new MoveCard(TBL_MOVE_CARD.GetEntity(1)));
        playerHandler.Enqueue(new AttackCard(TBL_ATTACK_CARD.GetEntity(1)));


        enemyHandler.Enqueue(new MoveCard(TBL_MOVE_CARD.GetEntity(3)));
        enemyHandler.Enqueue(new MoveCard(TBL_MOVE_CARD.GetEntity(2)));
        enemyHandler.Enqueue(new MoveCard(TBL_MOVE_CARD.GetEntity(3)));
    }

    private void Start()
    {

        UpdateHandler();

        SetCard();
    }

    public void AddCard(Card card)
    {
        int i;
        CardComponent cc = null;
        for (i=0; i<cardPos.Length; i++)
        {
            cc = cardPos[i].GetComponent<CardComponent>();
            if (cc.card == null)
            {
                cardList.Insert(i, card);
                break;
            }
        }
        cc.SetCard(card);
    }

    public void UpdateCardPos()
    {
        for(int i=0; i<cardPos.Length; i++)
        {
            if (i < cardList.Count)
                cardPos[i].GetComponent<CardComponent>().SetCard(cardList[i]);
            else
                cardPos[i].GetComponent<CardComponent>().DeleteCard();
        }
    }

    public void UpdateHandler()
    {
        Queue<Card> t = new Queue<Card>(playerHandler); //clone
        for(int i=0;i<playerHandlerPos.Length; i++)
        {
            if (i < playerHandler.Count)
                playerHandlerPos[i].GetComponent<CardComponent>().SetCard(t.Dequeue());
            else
                playerHandlerPos[i].GetComponent<CardComponent>().DeleteCard();
        }

        t = new Queue<Card>(enemyHandler); //clone
        for (int i = 0; i < enemyHandlerPos.Length; i++)
        {
            if (i < enemyHandler.Count)
                enemyHandlerPos[i].GetComponent<CardComponent>().SetCard(t.Dequeue());
            else
                enemyHandlerPos[i].GetComponent<CardComponent>().DeleteCard();
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
                cardList.Add(card);
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
                cardList.Add(card);
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
                cardList.Add(card);
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
                cardList.Add(card);
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
                cardList.Add(card);
                startCount--;
            }
        }
        
        

        Shuffle(cardList);

        //for(int i=0; i<list.Count; i++)
        //{
        //    cardPos[i].GetComponent<CardComponent>().SetCard(list[i]);
        //}

        UpdateCardPos();
    }


    private bool CheckDuplicate(Card target)
    {
        foreach(Card card in cardList)
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
