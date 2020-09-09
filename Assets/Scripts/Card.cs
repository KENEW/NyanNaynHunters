using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public string name;
    public string comment;
    public float percent;

    public Card(string name, string comment, float percent)
    {
        this.name = name;
        this.comment = comment;
        this.percent = percent;
    }
    public abstract void UseCard();
}
