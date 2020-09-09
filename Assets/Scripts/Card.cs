using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public string cardName;
    public string comment;
    public float percent;

    public Card(string cardName, string comment, float percent)
    {
        this.cardName = cardName;
        this.comment = comment;
        this.percent = percent;
    }
    public abstract void UseCard(int index);
}
