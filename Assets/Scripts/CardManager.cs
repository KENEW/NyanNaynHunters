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



    public Card GetRandomCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        SortedList percent = new SortedList();
        percent.Add(CardSetting.AttackCardDropPercent, CardType.AttackCard);
        percent.Add(CardSetting.EnergyCardDropPercent, CardType.EnergyCard);
        percent.Add(CardSetting.GuardCardDropPercent, CardType.GuardCard);
        percent.Add(CardSetting.HealCardDropPercent, CardType.HealCard);
        percent.Add(CardSetting.MoveCardDropPercent, CardType.MoveCard);

        float cumulative = 0f;
        for (int i = 0; i < percent.Count; i++)
        {
            cumulative += (float)percent.GetKey(i);
            if (value <= cumulative)
            {
                card = SelectCard((CardType)percent.GetByIndex(i));
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
        SortedList<float, TBL_ATTACK_CARD> li = new SortedList<float, TBL_ATTACK_CARD>();
        for (int i = 0; i < TBL_ATTACK_CARD.CountEntities; i++)
        {
            TBL_ATTACK_CARD data = TBL_ATTACK_CARD.GetEntity(i);
            li.Add(data.Percent, data);
        }

        float cumulative = 0f;
        for (int i = 0; i < li.Count; i++)
        {
            cumulative += li[i].Percent;
            if (value <= cumulative)
            {
                card = new AttackCard(li[i]);
            }
        }
        return card;
    }

    private Card RandomEnergyCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        SortedList<float, TBL_ENERGY_CARD> li = new SortedList<float, TBL_ENERGY_CARD>();
        for (int i = 0; i < TBL_ENERGY_CARD.CountEntities; i++)
        {
            TBL_ENERGY_CARD data = TBL_ENERGY_CARD.GetEntity(i);
            li.Add(data.Percent, data);
        }

        float cumulative = 0f;
        for (int i = 0; i < li.Count; i++)
        {
            cumulative += li[i].Percent;
            if (value <= cumulative)
            {
                card = new EnergyCard(li[i]);
            }
        }
        return card;
    }

    private Card RandomGuardCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        SortedList<float, TBL_GUARD_CARD> li = new SortedList<float, TBL_GUARD_CARD>();
        for (int i = 0; i < TBL_GUARD_CARD.CountEntities; i++)
        {
            TBL_GUARD_CARD data = TBL_GUARD_CARD.GetEntity(i);
            li.Add(data.Percent, data);
        }

        float cumulative = 0f;
        for (int i = 0; i < li.Count; i++)
        {
            cumulative += li[i].Percent;
            if (value <= cumulative)
            {
                card = new GuardCard(li[i]);
            }
        }
        return card;
    }

    private Card RandomHealCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        SortedList<float, TBL_HEAL_CARD> li = new SortedList<float, TBL_HEAL_CARD>();
        for (int i = 0; i < TBL_HEAL_CARD.CountEntities; i++)
        {
            TBL_HEAL_CARD data = TBL_HEAL_CARD.GetEntity(i);
            li.Add(data.Percent, data);
        }

        float cumulative = 0f;
        for (int i = 0; i < li.Count; i++)
        {
            cumulative += li[i].Percent;
            if (value <= cumulative)
            {
                card = new HealCard(li[i]);
            }
        }
        return card;
    }

    private Card RandomMoveCard()
    {
        int value = Random.Range(0, 100);
        Card card = null;
        SortedList<float, TBL_MOVE_CARD> li = new SortedList<float, TBL_MOVE_CARD>();
        for (int i = 0; i < TBL_MOVE_CARD.CountEntities; i++)
        {
            TBL_MOVE_CARD data = TBL_MOVE_CARD.GetEntity(i);
            li.Add(data.Percent, data);
        }

        float cumulative = 0f;
        for (int i = 0; i < li.Count; i++)
        {
            cumulative += li[i].Percent;
            if (value <= cumulative)
            {
                card = new MoveCard(li[i]);
            }
        }
        return card;
    }
}
