using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCard : Card
{
    public int damageReduction;

    public GuardCard(TBL_GUARD_CARD gc) : base(gc.name, gc.Comment, gc.Percent)
    {
        this.damageReduction = gc.DamageReduction;
    }
    public override void UseCard()
    {

    }
}
