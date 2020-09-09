using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public string name;
    public string comment;
    public float percent;
    public abstract void UseCard();
}
