using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card
{
    public int damage;
    public int energyCost;
    public List<int> positions;

    public AttackCard(TBL_ATTACK_CARD ac) : base(ac.name, ac.Comment, ac.Percent)
    {
        this.damage = ac.Damage;
        this.energyCost = ac.EnergyCost;
        this.positions = ac.Positions;
    }
    public override void UseCard(int index)
    {
        
    }
}
