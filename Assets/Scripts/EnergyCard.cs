using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCard : Card
{
    public int energyIncrease;
    public EnergyCard(TBL_ENERGY_CARD ec, string comment = "") : base(ec.name, comment, ec.Percent)
    {
        this.energyIncrease = ec.EnergyIncrease;
    }
    public override void UseCard()
    {

    }
}
