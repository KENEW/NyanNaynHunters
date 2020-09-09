using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Down,
    Up,
    Left,
    Right
};

public class MoveCard : Card
{
    public int distance;
    public MoveType moveType;

    public MoveCard(TBL_MOVE_CARD mc) : base(mc.name, mc.Comment, mc.Percent)
    {
        this.distance = mc.Distance;
        this.moveType = mc.MoveType;
    }
    public override void UseCard(int index)
    {

    }
}
