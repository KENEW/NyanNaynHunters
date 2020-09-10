using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoSingleton<CardManager>
{
    public enum CardType
    {
        AttackCard,
        EnergyCard,
        GuardCard,
        HealCard,
        MoveCard
    };

    class Temp
    {
        public float percent;
        public CardType cardType;
        public Card card;

        public Temp(float percent, CardType cardType, Card card = null)
        {
            this.percent = percent;
            this.cardType = cardType;
            this.card = card;
        }
    }

    public Card GetRandomCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        List<Temp> list = new List<Temp>();

        list.Add(new Temp(CardSetting.AttackCardDropPercent, CardType.AttackCard));
        list.Add(new Temp(CardSetting.EnergyCardDropPercent, CardType.EnergyCard));
        list.Add(new Temp(CardSetting.GuardCardDropPercent, CardType.GuardCard));
        list.Add(new Temp(CardSetting.HealCardDropPercent, CardType.HealCard));
        list.Add(new Temp(CardSetting.MoveCardDropPercent, CardType.MoveCard));

        list.Sort(delegate (Temp a, Temp b)
        {
            return a.percent.CompareTo(b.percent);
        });

        float cumulative = 0f;
        for (int i = 0; i < list.Count; i++)
        {
            cumulative += list[i].percent;
            if (value <= cumulative)
            {
                card = SelectCard(list[i].cardType);
            }
        }
        return card;
    }

    private Card SelectCard(CardType cardType)
    {
        Card card = null;
        if (cardType == CardType.AttackCard) card = RandomAttackCard();
        else if (cardType == CardType.EnergyCard) card = RandomEnergyCard();
        else if (cardType == CardType.GuardCard) card = RandomGuardCard();
        else if (cardType == CardType.HealCard) card = RandomHealCard();
        else card = RandomMoveCard();
        return card;
    }

    private Card RandomAttackCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        List<Temp> list = new List<Temp>();
        for (int i = 0; i < TBL_ATTACK_CARD.CountEntities; i++)
        {
            TBL_ATTACK_CARD data = TBL_ATTACK_CARD.GetEntity(i);
            AttackCard c = new AttackCard(data);
            list.Add(new Temp(c.percent, CardType.AttackCard, c));
        }

        float cumulative = 0f;
        for (int i = 0; i < list.Count; i++)
        {
            cumulative += list[i].percent;
            if (value <= cumulative)
            {
                card = list[i].card;
            }
        }
        return card;
    }

    private Card RandomEnergyCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        List<Temp> list = new List<Temp>();
        for (int i = 0; i < TBL_ENERGY_CARD.CountEntities; i++)
        {
            TBL_ENERGY_CARD data = TBL_ENERGY_CARD.GetEntity(i);
            EnergyCard c = new EnergyCard(data);
            list.Add(new Temp(c.percent, CardType.EnergyCard, c));
        }

        float cumulative = 0f;
        for (int i = 0; i < list.Count; i++)
        {
            cumulative += list[i].percent;
            if (value <= cumulative)
            {
                card = list[i].card;
            }
        }
        return card;
    }

    private Card RandomGuardCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        List<Temp> list = new List<Temp>();
        for (int i = 0; i < TBL_GUARD_CARD.CountEntities; i++)
        {
            TBL_GUARD_CARD data = TBL_GUARD_CARD.GetEntity(i);
            GuardCard c = new GuardCard(data);
            list.Add(new Temp(c.percent, CardType.EnergyCard, c));
        }

        float cumulative = 0f;
        for (int i = 0; i < list.Count; i++)
        {
            cumulative += list[i].percent;
            if (value <= cumulative)
            {
                card = list[i].card;
            }
        }
        return card;
    }

    private Card RandomHealCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        List<Temp> list = new List<Temp>();
        for (int i = 0; i < TBL_HEAL_CARD.CountEntities; i++)
        {
            TBL_HEAL_CARD data = TBL_HEAL_CARD.GetEntity(i);
            HealCard c = new HealCard(data);
            list.Add(new Temp(c.percent, CardType.EnergyCard, c));
        }

        float cumulative = 0f;
        for (int i = 0; i < list.Count; i++)
        {
            cumulative += list[i].percent;
            if (value <= cumulative)
            {
                card = list[i].card;
            }
        }
        return card;
    }

    private Card RandomMoveCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        List<Temp> list = new List<Temp>();
        for (int i = 0; i < TBL_MOVE_CARD.CountEntities; i++)
        {
            TBL_MOVE_CARD data = TBL_MOVE_CARD.GetEntity(i);
            MoveCard c = new MoveCard(data);
            list.Add(new Temp(c.percent, CardType.EnergyCard, c));
        }

        float cumulative = 0f;
        for (int i = 0; i < list.Count; i++)
        {
            cumulative += list[i].percent;
            if (value <= cumulative)
            {
                card = list[i].card;
            }
        }
        return card;
    }
}
