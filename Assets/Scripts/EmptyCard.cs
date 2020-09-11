using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCard : Card
{
    public EmptyCard() : base("비어있음", "empty", 0)
    {
       
    }
    public override void UseCard(int index = 0)
    {
        
    }
}
