using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCard : Card
{
    public int HPIncrease;
    public int energyCost;

    public HealCard(TBL_HEAL_CARD hc, string comment = "") : base(hc.name, comment, hc.Percent)
    {
        this.HPIncrease = hc.HPIncrease;
        this.energyCost = hc.EnergyCost;
    }
    public override void UseCard()
    {

    }
}
