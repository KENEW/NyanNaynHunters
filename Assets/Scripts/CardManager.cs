using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoSingleton<CardManager>
{
    public Card GetRandomCard()
    {
        int value = Random.Range(1, 100);
        bool[] arr = new bool[5];
        if (CardSetting.AttackCardDropPercent <= value) arr[0] = true;
        if (CardSetting.EnergyCardDropPercent <= value) arr[1] = true;
        if (CardSetting.GuardCardDropPercent <= value) arr[2] = true;
        if (CardSetting.HealCardDropPercent <= value) arr[3] = true;
        if (CardSetting.MoveCardDropPercent <= value) arr[4] = true;
        return null;
    }
}
