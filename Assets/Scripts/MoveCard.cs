using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCard : Card
{
    public int distance;
    public float percent;
    public enum MoveType
    {
        Down,
        Up,
        Left,
        Right
    };
}
